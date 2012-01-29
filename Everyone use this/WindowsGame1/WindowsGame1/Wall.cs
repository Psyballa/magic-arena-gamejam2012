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
        int health;
        Fixture wallFixture;
        Texture2D wallTex;

        public Wall(World gameWorld, Vector2 location, KingsOfAlchemy game) : base(gameWorld)
        {
            //Loading content in the constructor for simplicity's sake because the content manager is initialized by the time the stage is created
            health = 100;
            wallTex = game.Content.Load<Texture2D>("WallTile");
            location.X *= wallTex.Width;
            location.Y *= wallTex.Height;
            Position = location;

            wallFixture = FixtureFactory.AttachRectangle(wallTex.Width, wallTex.Height, 1, new Vector2(), this);
            wallFixture.CollisionCategories = Category.Cat1;

            wallFixture.OnCollision += _OnCollision;
        }

        public bool _OnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat1)
            {
                //Do thing that happens when player hits cat1
            }
            if (fix2.CollisionCategories == Category.Cat2)
            {
                //repeat this for every unique case
            }
            return true;
        }

        public void Update()
        {
            if (health <= 0)
            {
                //destroy self, I forget how to do that right now, im tired
            }
        }

        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (health > 0)
                spriteBatch.Draw(wallTex, new Rectangle((int)Position.X, (int)Position.Y, wallTex.Width, wallTex.Height), Color.White);
        }
    }
}
