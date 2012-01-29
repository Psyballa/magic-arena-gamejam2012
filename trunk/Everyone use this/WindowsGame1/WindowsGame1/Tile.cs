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
    class Tile : Body
    {
        float health;
        float prevhealth;
        float maxhealth;
        Fixture tileFixture;
        Texture2D tileTex;

        float breakstages = 7;

        bool dead = false;

        SoundEffect breakSound;

        KingsOfAlchemy game;


        public Tile(World gameWorld, Vector2 location, KingsOfAlchemy game, Vector2 offset) : base(gameWorld)
        {
            //Loading content in the constructor for simplicity's sake because the content manager is initialized by the time the stage is created
            health = 100;
            prevhealth = health;
            maxhealth = health;

            this.game = game;

            tileTex = game.Content.Load<Texture2D>("Tiles/MarbleTilesBreak0");
            breakSound = game.Content.Load<SoundEffect>("Tiles/FloorBreaking");
            location.X *= tileTex.Width;
            location.Y *= tileTex.Height;
            location += offset;
            Position = location;
            tileFixture = FixtureFactory.AttachRectangle(tileTex.Width, tileTex.Height, 1, new Vector2(), this);
            tileFixture.CollisionCategories = Category.Cat2;
            tileFixture.CollidesWith = Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6 | Category.Cat8;

            tileFixture.CollisionCategories = Category.Cat2;
            tileFixture.OnCollision += _OnCollision;
        }

        public bool _OnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat3)
            {
                //UNCOMMENT TO TEST COLLISION BOXES
                //health -= 1;
            }

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
                tileTex = game.Content.Load<Texture2D>("Tiles/MarbleTilesBreak" + currstage.ToString());
            }

            prevhealth = currstage;

            //Makes the tile inactive if it is destroyed
            if (health <= 0 && !dead)
            {
                dead = true;
                tileFixture.CollisionCategories = Category.None;
                //breakSound.Play();
            }
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (health > 0)
                spriteBatch.Draw(tileTex, new Rectangle((int)Position.X, (int)Position.Y, tileTex.Width, tileTex.Height), Color.White);
        }
    }
}
