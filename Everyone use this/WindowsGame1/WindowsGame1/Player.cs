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
     * Category 2: Floor
     * Category 3: Player
     * Category 4: Fire Cone
     * Category 5: Water Beam
     * Category 6: Rock
     * Category 7: Midair Rock
     * Category 8: Tornado
     */


    public class Player : Body
    {
        public float damage;
        float playerSize = 5;
        Texture2D playerTex;
        Controller playerController;
        public Fixture playerFixture;
        

        //Attack stuff
        Element currentEquip = Element.fire;
        float leftcharge = 0;
        float rightcharge = 0;

        //Player Stats
        private const float playerMass = 10f;
        private const float playerRotate = 0.15f;
        private const float MetersInPx = 64f;
        private const float playerAccel = 0.4f;

        //Flags
        public bool dead = false;
        bool fallingFlag = false;

        KingsOfAlchemy game;

        public Player(World gameWorld, int playerNum, KingsOfAlchemy game) : base(gameWorld)
        {
            //Merged this with loadContent for simplicity
            this.game = game;
            switch (playerNum)
            {
                //Positions will likely need to be changed based on world size
                case 1:
                    playerController = new Controller(PlayerIndex.One);
                    playerTex = game.Content.Load<Texture2D>("Player1");
                    Position = new Vector2(60, 60);
                    break;
                case 2:
                    playerController = new Controller(PlayerIndex.Two);
                    playerTex = game.Content.Load<Texture2D>("Player2");
                    Position = new Vector2(400, 60);
                    break;
                case 3:
                    playerController = new Controller(PlayerIndex.Three);
                    playerTex = game.Content.Load<Texture2D>("Player3");
                    Position = new Vector2(60, 400);
                    break;
                case 4:
                    playerController = new Controller(PlayerIndex.Four);
                    playerTex = game.Content.Load<Texture2D>("Player4");
                    Position = new Vector2(400, 400);
                    break;
            }
            damage = 0;

            // load player sprite
            Vector2 playerOrigin;
            playerOrigin.X = playerTex.Width / 2;
            playerOrigin.Y = playerTex.Height / 2;

            playerFixture = FixtureFactory.AttachCircle(playerSize, 1, this, new Vector2(-playerTex.Width/2, -playerTex.Height/2));
            playerFixture.Body.BodyType = BodyType.Dynamic;

            playerFixture.CollisionCategories = Category.Cat3;
            playerFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6 | Category.Cat8;

            playerFixture.OnCollision += playerOnCollision;

            playerFixture.Restitution = 0.9f;   //Energy Retained from bouncing
            playerFixture.Friction = 0.1f;      //friction with other objects

        }

        public bool playerOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            switch(fix2.CollisionCategories){
                case Category.Cat1:                         //Wall
                    return true;
                case Category.Cat2:                         //Floor
                    fallingFlag = false;
                    return false;
                case Category.Cat3:                         //Player
                    return true;
                case Category.Cat4:                         //Fire spray
                    damage += 0.05f;
                    return false;
                case Category.Cat5:                         //Water beam
                    damage += 0.01f;
                    return true;
                case Category.Cat6:                         //Rock
                    damage += 0.05f;
                    LinearVelocity = LinearVelocity + Vector2.Normalize(fix1.Body.Position - Position) * 2 * (1 + damage);
                    return false;
                case Category.Cat8:                         //Tornado
                    LinearVelocity = 
                        Vector2.Transform(
                        LinearVelocity,
                        Matrix.CreateRotationZ((float)Math.Max(0, 30 - Vector2.Distance(fix1.Body.Position, fix2.Body.Position)) * 1 + damage));
                    return false;
                default:
                    fallingFlag = true;
                    return false;

            }
        }

        public void Update()
        {
            if (dead) return;
            goDoThis elementChange = playerController.getEquipChange();
            //if(elementChange = goDoThis.equipAir && 
            // Call methods to get info from controller
            // Do Stuff (this will include methods to shoot attacks)
            Vector2 newv = new Vector2();
            if (playerController.getLeftCharge())
            {
                game.attacks.Add(new Water(playerController.getRotation(), Position, game));
            }
            float rotation = playerController.getRotation();
            newv = playerController.getMovement();
            LinearVelocity = newv;
            //This is set to false in the collision detection if the player is safely standing on a tile, and set to false after each update
            if (fallingFlag)
            {
                dead = true;
            }
           
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (dead) return;
            Vector2 playerOrigin;
            playerOrigin.X = playerTex.Width/2;
            playerOrigin.Y = playerTex.Height/2;
          

            spriteBatch.Draw(playerTex,
                new Rectangle((int)Position.X - playerTex.Width / 2, (int)Position.Y - playerTex.Height / 2, 
                playerTex.Width, playerTex.Height),
                new Rectangle(0,0,playerTex.Width,playerTex.Height),
                Color.White,
                playerController.getRotation(), 
                playerOrigin, 
                SpriteEffects.None,
                0f);
        }


            
    }
}
