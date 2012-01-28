using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Controller(int playerNum)
        {
            // Assign the correct gamepad to the given player
        }

        public goDoThis getEquipChange()
        {
            // logic to return what the controller says to do given the control scheme
        }

        public Vector2 getMovement()
        {
            //return the Vector2 that is the impulse to be applied to the player's body
        }

        public float getRotation()
        {
            //Return which way the controller says the player should be facing
        }

        public bool getLeftCharge()
        {
            // return true if the player should be charging his left shot
        }

        public bool getRightCharge()
        {
            // return true if the player should be charging his right shot
        }

        public void setControls(controls newControls)
        {
            theControls = newControls;
        }
    }
}
