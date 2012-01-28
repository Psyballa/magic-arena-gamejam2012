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
            if (fix2.CollisionFilter.CollisionCategories == Category.Cat1)
            {
                //Do thing that happens when player hits cat1
            }
            if (fix2.CollisionFilter.CollisionCategories == Category.Cat2)
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
    }
}
