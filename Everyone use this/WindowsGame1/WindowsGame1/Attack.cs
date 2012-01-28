using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
namespace WindowsGame1
{
    class Attack
    {
        //this should be a class that can be extended by all of the different attacks, Ideally each attack will be such that the player can create an 
        //instance of it with starting parameters (direction and such) and the object will handle itself from there. (collision, running, destroying itself when it's done)
        // I am really tired so i can't really think much about what this will be
        //just do some smart at it
    }
}
