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
    class Wall
    {
        /*Note on collision categories (Arbitrarily defined based on what was already in the code):
         * Category 1: Wall
         * Category 2: Floor
         * Category 3: Player
         * Category 4: Projectile
         * -C
         */
        int health;
        Fixture wallFixture;
        World wallWorld;
        Texture2D wallTex;

        public Wall(World gameWorld, Vector2 location)
        {
            wallWorld = gameWorld;
            health = 100;
        }

        public void loadContent()
        {
            //Make walltex a thing

            //initialize the fixture to some rectangle that fits the texture dimensions

            wallFixture.OnCollision += OnCollision;
        }

        public bool OnCollision(Fixture fix1, Fixture fix2, Contact con)
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
                spriteBatch.Draw(tileTex, new Rectangle((int)Position.X, (int)Position.Y, tileTex.Width, tileTex.Height), Color.White);
        }
    }
}
