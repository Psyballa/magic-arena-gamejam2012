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
    public class Attack : Body
    {
        public List<ParticleSystem> particleSystem = new List<ParticleSystem>();
        public float damage, radius, impulse;
        public Fixture attackFixture;
        public Player owner;
        public float speed;
        public float angle;


        public Attack(World gameWorld, float d, float r, float i, float speed, Vector2 p, Player owner)
            : base(gameWorld)
        {
            this.damage = d;
            this.radius = r;
            this.Position = p;
            this.owner = owner;
            this.speed = speed;
            this.angle = d;
            IsStatic = false;
            attackFixture = FixtureFactory.AttachCircle(radius, i, this);
            LinearVelocity = new Vector2((float)(speed * Math.Sin(angle)), (float)(speed * Math.Cos(angle)));

            
        }

        public virtual void update(GameTime gameTime)
        {
            foreach (var p in particleSystem)
            {
                p.position = Position;
            }
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var p in particleSystem)
            {
                p.draw(gameTime, spriteBatch);
            }
        }

    }
}
