using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace WindowsGame1
{
    class Wall : Body
    {
        /*Note on collision categories (Arbitrarily defined based on what was already in the code):
         * Category 1: Wall 
         * Category 2: Floor
         * Category 3: Player
         * Category 4: Fire Cone
         * Category 5: Water Beam
         * Category 6: Rock
         * Category 7: Midair Rock
         * Category 8: Tornado
         */
        float health;
        float maxhealth;
        float prevhealth;
        Fixture wallFixture;
        Texture2D wallTex;
        SoundEffect breakSound;
        float breakstages = 7;
        KingsOfAlchemy game;

        bool dead = false;

        public Wall(World gameWorld, Vector2 location, KingsOfAlchemy game, Vector2 offset) : base(gameWorld)
        {
            //Loading content in the constructor for simplicity's sake because the content manager is initialized by the time the stage is created
            health = 100;
            prevhealth = health;
            maxhealth = health;
            this.game = game;
            wallTex = game.Content.Load<Texture2D>("Walls/WallTileBreak0");
            location.X *= wallTex.Width;
            location.Y *= wallTex.Height;
            location += offset;
            Position = location;

            wallFixture = FixtureFactory.AttachRectangle(wallTex.Width, wallTex.Height, 1, new Vector2(), this);
            wallFixture.CollisionCategories = Category.Cat1;
            wallFixture.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6 | Category.Cat7 | Category.Cat8;

            wallFixture.OnCollision += _OnCollision;

            breakSound = game.Content.Load<SoundEffect>("Tiles/FloorBreaking");
        }

        public bool _OnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat4)
            {
                health -= 1;
            }
            if (fix2.CollisionCategories == Category.Cat5)
            {
                health -= 0.5f;
            }
            if (fix2.CollisionCategories == Category.Cat6)
            {
                health -= 10;
            }
            if (fix2.CollisionCategories == Category.Cat8)
            {
                health -= 0.5f;
            }
            return false;
        }

        public void Update()
        {
            int currstage = (int)((maxhealth - health) / (maxhealth / breakstages));
            if (currstage < 0)
            {
                currstage = 0;
            }
            if (currstage > breakstages - 1)
            {
                currstage = (int)breakstages - 1;
            }
            if (currstage > prevhealth)
            {
                wallTex = game.Content.Load<Texture2D>("Walls/WallTileBreak" + currstage.ToString());
            }

            prevhealth = currstage;

            //Makes the tile inactive if it is destroyed
            if (health <= 0 && !dead)
            {
                dead = true;
                wallFixture.CollisionCategories = Category.None;
                //breakSound.Play();
            }
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (health > 0)
                spriteBatch.Draw(wallTex, new Rectangle((int)Position.X, (int)Position.Y, wallTex.Width, wallTex.Height), Color.White);
        }

    }
}
