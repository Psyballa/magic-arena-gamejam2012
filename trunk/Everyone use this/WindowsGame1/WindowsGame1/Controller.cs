using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;

namespace WindowsGame1
{
    public enum goDoThis
    {
        equipFire, equipEarth, equipAir, equipWater, rotateEquipRight, rotateEquipLeft, doNothing
    }

    public enum controls
    {
        quickfire, comboAttack
    }

    class Controller
    {
        controls theControls;
        PlayerIndex controller;
        GamePadState prevState;
        float prevRotation = 0;
        float prevRotation2 = 0;
        bool lastSentRightCharge = false;
        bool lastSentLeftCharge = false;

        public Controller(PlayerIndex playerNum)
        {
            // Assign the correct gamepad to the given player
            this.controller = playerNum;
            prevState = GamePad.GetState(controller);
        }

        public goDoThis getEquipChange()
        {
            // logic to return what the controller says to do given the control scheme
            GamePadState state = GamePad.GetState(controller);
            if (state.IsButtonDown(Buttons.A))
            {
                prevState = state;
                return goDoThis.equipAir;
            }
            if (state.IsButtonDown(Buttons.B))
            {
                prevState = state;
                return goDoThis.equipWater;
            }
            if (state.IsButtonDown(Buttons.X))
            {
                prevState = state;
                return goDoThis.equipFire;
            }
            if (state.IsButtonDown(Buttons.Y))
            {
                prevState = state;
                return goDoThis.equipEarth;
            }
            if (state.IsButtonDown(Buttons.LeftShoulder) && !prevState.IsButtonDown(Buttons.LeftShoulder))
            {
                prevState = state;
                return goDoThis.rotateEquipLeft;
            }
            if (state.IsButtonDown(Buttons.RightShoulder) && !prevState.IsButtonDown(Buttons.RightShoulder))
            {
                prevState = state;
                return goDoThis.rotateEquipRight;
            }

            return goDoThis.doNothing;

        }

        public Vector2 getMovement()
        {
            //return the Vector2 that is the impulse to be applied to the player's body
            return new Vector2(GamePad.GetState(controller).ThumbSticks.Left.X, -GamePad.GetState(controller).ThumbSticks.Left.Y);
        }

        public float getRotation()
        {
            //Return which way the controller says the player should be facing
            if (GamePad.GetState(controller).ThumbSticks.Right.Y == 0 && GamePad.GetState(controller).ThumbSticks.Right.X == 0) return prevRotation;
            prevRotation = (float)-Math.Atan2(GamePad.GetState(controller).ThumbSticks.Right.Y, GamePad.GetState(controller).ThumbSticks.Right.X) + (float)Math.PI / 2;
            return prevRotation;
        }
        public float getRotation2()
        {
            //Return which way the controller says the player should be facing for the purposes of projectiles (this is different for some reason)
            if (GamePad.GetState(controller).ThumbSticks.Right.Y == 0 && GamePad.GetState(controller).ThumbSticks.Right.X == 0) return prevRotation2;
            prevRotation2 = (float)Math.Atan2(GamePad.GetState(controller).ThumbSticks.Right.Y, GamePad.GetState(controller).ThumbSticks.Right.X) + (float)Math.PI / 2;
            return prevRotation2;
        }

        public bool getLeftCharge()
        {
            // return true if the player should be charging his left shot
            if(lastSentLeftCharge){
                if(GamePad.GetState(controller).Triggers.Left <= 0.2)
                {
                    lastSentLeftCharge = false;
                }
            }
            else{
                if(GamePad.GetState(controller).Triggers.Left > 0.2)
                {
                    lastSentLeftCharge = true;
                    return true;
                }
            }
            return false;
        }

        public bool getRightCharge()
        {
            return GamePad.GetState(controller).Triggers.Right > 0.2;
            // return true if the player should be charging his right shot
        }

        public void setControls(controls newControls)
        {
            theControls = newControls;
        }
    }
}
