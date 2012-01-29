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
        List<ParticleSystem> particleSystem = new List<ParticleSystem>();
        float damage, radius, impulse;
        Vector2 velocity;
        public Fixture attackFixture;


        public Attack(World gameWorld, List<ParticleSystem> partSystem, float d, float r, float i)
            : base(gameWorld)
        {
            this.particleSystem = partSystem;
            this.damage = d;
            this.radius = r;
            this.impulse = i;
            attackFixture = FixtureFactory.AttachCircle(radius, 1, this);
        }


    }
}
