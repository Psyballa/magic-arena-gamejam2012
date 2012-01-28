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
         *      collides with 4 to take constant damage and stop the cone
         *      collides with 5 to take constant damage and stop the beam
         *      collides with 6&7 to take instantaneous damage
         *      collides with 8 to take instantaneous damage
         * Category 2: Floor
         *      Collides with 4 to take constant damage
         *      Collides with 5 to take much less constant damage
         *      Collides with 6 to take instantaneous damage and destroy it
         *      Collides with 8 to take constant damage
         * Category 3: Player
         *      Collides with 1 to bounce off
         *      Collides with 4 to take constant damage
         *      Collides with 5 to take damage and stop the beam
         *      collides with 6 to take immediate damage (floor destroys it)
         *      collides with 8 to take constant damage
         * Category 4: Fire Cone
         *      Collision handled elsewhere
         * Category 5: Water Beam
         *      Collision handled elsewhere
         * Category 6: Rock
         *      Collides with 1 to disperse
         *      Collides with 2 to disperse
         *      Collides with 3 to disperse
         * Category 7: Midair Rock
         *      Collides with 1 to disperse
         * Category 8: Tornado
         *      Collides with 1 to disperse
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
