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

    /*Note on collision categories (Arbitrarily defined based on what was already in the code):
     * Category 1: Wall 
     *      Collides with 3 with 3 bouncing off
     *      collides with 4 to take constant damage and stop the cone
     *      collides with 5 to take constant damage and stop the beam
     *      collides with 6&7 to take instantaneous damage, destroy the rock, and create a dust effect
     *      collides with 8 to disperse it and take instantaneous damage
     * Category 2: Floor
     *      Collides with 4 to take constant damage
     *      Collides with 5 to take much less constant damage
     *      Collides with 6 to take instantaneous damage and destroy it
     *      Collides with 8 to take constant damage
     * Category 3: Player
     *      Collides with 4 to take constant damage
     *      Collides with 5 to take damage and stop the beam
     *      collides with 6 to take immediate damage (floor destroys it)
     *      collides with 8 to take constant damage
     * Category 4: Fire Cone
     *      Collision handled elsewhere
     * Category 5: Water Beam
     *      Collision handled elsewhere
     * Category 6: Rock
     * Category 7: Midair Rock
     *      Collides with 8 to accelerate clockwise
     * Category 8: Tornado
     */
    class Tile : Body
    {
        int health;
        Fixture tileFixture;
        World tileWorld;
        Texture2D tileTex;
        Vector2 location;

        public Tile(World gameWorld, Vector2 location) : base(gameWorld)
        {
            this.location = location;
            tileWorld = gameWorld;
            health = 100;
        }

        public void loadContent(KingsOfAlchemy game)
        {
            //Make walltex a thing

            //initialize the fixture to some rectangle that fits the texture dimensions
            tileTex = game.Content.Load<Texture2D>("FloorTile");

            //Translate from tile coordinates to pixel coordinates
            location.X *= tileTex.Width;
            location.Y *= tileTex.Height;

            tileFixture.OnCollision += OnCollision;
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
