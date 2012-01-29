using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;


namespace WindowsGame1
{
    class Attack : Body
    {
        public enum attackElement 
        { 
            earth, water, air, fire 
        }
        List<ParticleSystem> particleSystem = new List<ParticleSystem>();
        float damage, radius, impulse;
        Vector2 velocity;
        public Fixture attackFixture;
        //public Body attackBody;
        

        public Attack (World gameWorld, List<ParticleSystem> partSystem, float d, float r, float i) : base(gameWorld)
        {
            this.particleSystem = partSystem;
            this.damage = d;
            this.radius = r;
            this.impulse = i;
        }
        //this should be a class that can be extended by all of the different attacks, Ideally each attack will be such that the player can create an 
        //instance of it with starting parameters (direction and such) and the object will handle itself from there. (collision, running, destroying itself when it's done)
        // I am really tired so i can't really think much about what this will be
        //just do some smart at it
    }
}
