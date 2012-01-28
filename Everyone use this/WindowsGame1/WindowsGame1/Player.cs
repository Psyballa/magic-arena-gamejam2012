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
    public enum Element
    {
        earth, water, air, fire
    }

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


    class Player : Body
    {
        private World playerWorld;
        int damage;
        float playerSize = 5;
        Texture2D playerTex;
        Controller playerController;
        public Fixture playerFixture {get; set;}
        Element currentEquip;


        //Player Stats
        private const float playerMass = 10f;
        private const float playerRotate = 0.15f;
        private const float MetersInPx = 64f;
        private const float playerAccel = 0.4f;

        public CollisionFilterDelegate ContactFilter;

        public Player(World gameWorld, int playerNum) : base(gameWorld)
        {
            
            playerWorld = gameWorld;

            switch (playerNum)
            {

                case 1:
                    //Set controller to player 1 and starting position to player 1 starting position
                    playerController = new Controller(PlayerIndex.One);
                    Position = new Vector2(10, 10);
                    break;
                case 2:
                    //Set controller to player 2 and starting position to player 2 starting position
                    playerController = new Controller(PlayerIndex.Two);
                    Position = new Vector2(10, 10);
                    break;
                case 3:
                    //Set controller to player 3 and starting position to player 3 starting position
                    playerController = new Controller(PlayerIndex.Three);
                    Position = new Vector2(10, 10);
                    break;
                case 4:
                    //Set controller to player 4 and starting position to player 4 starting position
                    playerController = new Controller(PlayerIndex.Four);
                    Position = new Vector2(10, 10);
                    break;
            }
            
            damage = 0;
        }

        public void loadContent(Game myGame)
        {
            // load player sprite

            playerFixture = FixtureFactory.AttachCircle(playerSize, 1, this);
            playerFixture.Body.BodyType = BodyType.Dynamic;

            playerFixture.CollisionCategories = Category.Cat3;
            playerFixture.CollidesWith = Category.Cat2 & ~Category.Cat3 & ~Category.Cat4 & ~Category.Cat5; //or whatever categories it will end up colliding with

            playerFixture.OnCollision += playerOnCollision;

            playerFixture.Restitution = 0.9f;   //Energy Retained from bouncing
            playerFixture.Friction = 0.1f;       //friction with other objects

        }

        public bool playerOnCollision(Fixture fix1, Fixture fix2, Contact con)
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

        public void UpdatePlayer()
        {
            // Call methods to get info from controller
            // Do Stuff (this will include methods to shoot attacks

        }



            
    }
}
