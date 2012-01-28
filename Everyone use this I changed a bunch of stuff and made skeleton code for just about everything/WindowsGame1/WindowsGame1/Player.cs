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
    public enum element
    {
        earth, water, air, fire
    }

    class Player
    {
        private World playerWorld;
        int damage;
        float playerSize = 5;
        Texture2D playerTex;
        Controller playerController;
        public Fixture playerFixture {get; set;}
        element currentEquip;


        //Player Stats
        private const float playerMass = 10f;
        private const float playerRotate = 0.15f;
        private const float MetersInPx = 64f;
        private const float playerAccel = 0.4f;

        public Player(World gameWorld, int playerNum)
        {
            playerWorld = gameWorld;
            switch (playerNum)
            {
                case 1:
                    //Set controller to player 1 and starting position to player 1 starting position
                    break;
                case 2:
                    //Set controller to player 2 and starting position to player 2 starting position
                    break;
                case 3:
                    //Set controller to player 3 and starting position to player 3 starting position
                    break;
                case 4:
                    //Set controller to player 4 and starting position to player 4 starting position
                    break;
            }
            damage = 0;
        }

        public void loadContent(Game myGame)
        {
            // load player sprite

            playerFixture = FixtureFactory.CreateCircle(playerWorld, playerSize, 1);
            playerFixture.Body.BodyType = BodyType.Dynamic;

            playerFixture.CollisionFilter.CollisionCategories = Category.Cat3;
            playerFixture.CollisionFilter.CollidesWith = Category.Cat2 & ~Category.Cat3 & ~Category.Cat4 & ~Category.Cat5; //or whatever categories it will end up colliding with

            playerFixture.OnCollision += playerOnCollision;

            playerFixture.Restitution = 0.9f;   //Energy Retained from bouncing
            playerFixture.Friction = 0.1f;       //friction with other objects

        }

        public bool playerOnCollision(Fixture fix1, Fixture fix2, Contact con)
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

        public void UpdatePlayer()
        {
            // Call methods to get info from controller
            // Do Stuff (this will include methods to shoot attacks

        }



            
    }
}
