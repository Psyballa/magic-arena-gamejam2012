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
        equipFire, equipEarth, equipAir, equipWater, rotateEquipRight, rotateEquipLeft
    }

    public enum controls
    {
        quickfire, comboAttack
    }

    class Controller
    {
        controls theControls;
        PlayerIndex controller;

        public Controller(PlayerIndex playerNum)
        {
            this.controller = playerNum;
            // Assign the correct gamepad to the given player
        }

        public goDoThis getEquipChange()
        {
            // logic to return what the controller says to do given the control scheme
            return new goDoThis();
        }

        public Vector2 getMovement()
        {
            //return the Vector2 that is the impulse to be applied to the player's body
            return new Vector2();
        }

        public float getRotation()
        {
            //Return which way the controller says the player should be facing
            return 0.0f;
        }

        public bool getLeftCharge()
        {
            // return true if the player should be charging his left shot
            return false;
        }

        public bool getRightCharge()
        {
            return false;
            // return true if the player should be charging his right shot
        }

        public void setControls(controls newControls)
        {
            theControls = newControls;
        }
    }
}
