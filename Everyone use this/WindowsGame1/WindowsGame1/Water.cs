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
    class Water : Attack
    {
        float lifetime = 140;
        float life = 0;
        public Water(float direction, Vector2 position, KingsOfAlchemy game, Player owner, float charge)
            : base(game, direction, 5, 3*charge, 150*(charge/10), position, owner)
        {
            attackFixture.CollisionCategories = Category.Cat5;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat4 | Category.Cat6 | Category.Cat8;
            particleSystem.Add(new ParticleSystem(0, (float)Math.PI * 2, Position, new Vector2(0.01f, 0), new Vector2(-0.003f, 0), 6, game.Content.Load<Texture2D>("Basicparticle"), -0.001f, 50, 80, Color.Aqua));
            attackFixture.OnCollision += waterOnCollision;
        }
        public bool waterOnCollision(Fixture fix1, Fixture fix2, Contact con)
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
