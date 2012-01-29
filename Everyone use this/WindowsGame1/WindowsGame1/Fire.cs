using System;
using System.Collections.Generic;
using System.Linq;
//using System.Math;
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
    class Fire : Attack
    {
        public const int MIN_FIRE_RANGE = 50;
        public const int MAX_FIRE_RANGE = 100;
        public const float MIN_FIRE_SPREAD = (float) Math.PI / 2;
        public const float MAX_FIRE_SPREAD = (float) Math.PI * 2;
        public const int MIN_FIRE_SHOTS = 3;
        public const int MAX_FIRE_SHOTS = 36;

        float lifetime = 140;
        float life = 0;


        public Fire(KingsOfAlchemy gameWorld, float rot, float speed, Vector2 p, Player owner)
            :base(gameWorld, rot, 2 , 4, speed, p, owner)
        {
            // init. width, init thickness, density
            attackFixture = FixtureFactory.AttachRectangle(10, 3.0f, 4, new Vector2(0,0), this);
            damage = 1;

            attackFixture.CollisionCategories = Category.Cat5;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat4 | Category.Cat6 | Category.Cat8;
            particleSystem.Add(new ParticleSystem(0, (float)Math.PI * 2, Position, new Vector2(0.01f, 0), new Vector2(-0.003f, 0), 6, game.Content.Load<Texture2D>("Basicparticle"), -0.001f, 50, 80, Color.Orange));
            attackFixture.OnCollision += fireOnCollision;

        }


        public bool fireOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat1)
                return true;
            return false;
        }

        public override void update(GameTime gameTime)
        {
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
