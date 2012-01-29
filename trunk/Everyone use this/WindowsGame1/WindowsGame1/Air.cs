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

    class Air : Attack
    {
        float lifetime = 140;
        float life = 0;
        public Air(float direction, Vector2 position, KingsOfAlchemy game, Player owner, float charge)
            : base(game, direction, 25, 8 * charge, 150 * charge, position, owner)
        {
            attackFixture.CollisionCategories = Category.Cat8;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6;
            particleSystem.Add(new airParticleSystem(
                0, 
                (float)Math.PI * 2, 
                Position,
                new Vector2(0, 0.05f),
                new Vector2(0, 0.0000f), 
                1, 
                game.Content.Load<Texture2D>("Basicparticle"), 
                -0.0001f, 
                1000, 
                1000,
                Color.White));
            attackFixture.OnCollision += airOnCollision;
        }
        public bool airOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat1)
                return true;
            return false;
        }
        public override void update(GameTime gameTime)
        {
            //LinearVelocity = LinearVelocity * 0.99f;
            if (!Awake) return;
            life += 1;
            if (life > lifetime)
            {
                Awake = false;
                CollisionCategories = Category.None;
                foreach (var a in particleSystem)
                {
                    a.destroy();
                }
            }
            base.update(gameTime);
        }
    }
}
