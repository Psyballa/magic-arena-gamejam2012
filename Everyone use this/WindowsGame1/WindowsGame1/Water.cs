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
        float lifetime = 40;
        float life = 0;
        public Water(float direction, Vector2 position, KingsOfAlchemy game, Player owner)
            : base(game.world, direction, 1, 3, 1, position, owner)
        {
            attackFixture.CollisionCategories = Category.Cat5;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat4 | Category.Cat5 | Category.Cat6 | Category.Cat8;
            particleSystem.Add(new ParticleSystem(0, (float)Math.PI*2, Position, new Vector2(0.01f, 0), new Vector2(-0.003f, 0), 1, game.Content.Load<Texture2D>("Basicparticle"), -0.01f, 50, 80));
        }
        public bool waterOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            return false;
        }
        public override void update(GameTime gameTime)
        {
            if (!Awake) return;
            life += 1;
            if (life > lifetime)
            {
                Awake = false;
                foreach (var a in particleSystem)
                {
                    a.destroy();
                }
            }
            base.update(gameTime);
        }
    }
}
