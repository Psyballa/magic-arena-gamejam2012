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

namespace WindowsGame1
{
    class Water : Attack
    {
        public Water(float direction, Vector2 position, KingsOfAlchemy game)
            : base(game.world, direction, 1, 3, 10, position)
        {
            attackFixture.CollisionCategories = Category.Cat3;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat3 | Category.Cat4 | Category.Cat6 | Category.Cat8;
            particleSystem.Add(new ParticleSystem(0, (float)Math.PI*2, Position, new Vector2(1, 0), new Vector2(-0.01f, 0), 4, game.Content.Load<Texture2D>("Basicparticle"), -0.01f, 50, 80));
        }
    }
}
