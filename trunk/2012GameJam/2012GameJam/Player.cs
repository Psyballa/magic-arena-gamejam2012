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

namespace _2012Gamejam
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

        public Player(int playerNum, World gameWorld)
        {
            GamePadState gamePadState;
            health = 100;
            playerBody.BodyType = BodyType.Dynamic;
            playerBodyShape = new CircleShape(1.0f, 1.0f);
            playerFix = playerBody.CreateFixture(playerBodyShape);
            gameWorld.AddBody(playerBody);
            playerNumber = playerNum;
            float rotAngle = rotationAngle;
            ////////////////////////////////////////////////////////// playerTex = ? also starting coords all based off player #
        }

        public void update()
        {

            float rotation;
            switch (playerNumber)
            {

                case 1:
                    gamePadState = GamePad.GetState(PlayerIndex.One);
                    break;
                case 2:
                    gamePadState = GamePad.GetState(PlayerIndex.Two);
                    break;
                case 3:
                    gamePadState = GamePad.GetState(PlayerIndex.Three);
                    break;
                case 4:
                    gamePadState = GamePad.GetState(PlayerIndex.Four);
                    break;
                default:
                    gamePadState = GamePad.GetState(PlayerIndex.One);
                    break;
            }
            rotation = Math.atan2(gamePadState.ThumbSticks.Right.Y / gamePadState.ThumbSticks.Right.X);

        }
    }
}