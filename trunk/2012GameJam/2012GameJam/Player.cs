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
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Collision;


namespace Gamejam
{
    class Player
    {
        int health;
        Body playerBody;
        Fixture playerFix;
        CircleShape playerBodyShape;
        Texture2D playerTex;
        int playerNumber;
        int controllerScheme;
        float rotationAngle;
        float velocityX, velocityY;
        GamePadState gamePad;

        public Player(int playerNum, World gameWorld)
        {
            GamePadState gamePadState = gamePad;
            health = 100;
            playerBody.BodyType = BodyType.Dynamic;
            playerBodyShape = new CircleShape(1.0f, 1.0f);
            playerFix = playerBody.CreateFixture(playerBodyShape);
            gameWorld.AddBody(playerBody);
            playerNumber = playerNum;
            float rotAngle = rotationAngle;
            float velX = velocityX;
            float velY = velocityY;
            ////////////////////////////////////////////////////////// playerTex = ? also starting coords all based off player #
        }

        public void update()
        {
            GamePadState gamePad;
            float rotation;
            float velX, velY;
            Vector2 velocity;
            switch (playerNumber)
            {

                case 1:
                    gamePad = GamePad.GetState(PlayerIndex.One);
                    break;
                case 2:
                    gamePad = GamePad.GetState(PlayerIndex.Two);
                    break;
                case 3:
                    gamePad = GamePad.GetState(PlayerIndex.Three);
                    break;
                case 4:
                    gamePad = GamePad.GetState(PlayerIndex.Four);
                    break;
                default:
                    gamePad = GamePad.GetState(PlayerIndex.One);
                    break;
            }
            rotation = (float)Math.Atan2((double)gamePad.ThumbSticks.Right.Y, (double)gamePad.ThumbSticks.Right.X);
            
            velX = (float)1.33 * gamePad.ThumbSticks.Left.X;
            velY = (float)1.33 * gamePad.ThumbSticks.Left.Y;

            velocity.X = (float)Math.Atan2((double)velY , (double)velX);
            velocity.Y = (float)Math.Atan2((double)velY, (double)velX);
        }
    }
}