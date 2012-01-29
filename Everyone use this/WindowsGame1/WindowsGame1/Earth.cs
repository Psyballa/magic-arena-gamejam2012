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
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;


namespace WindowsGame1
{
    class Earth : Attack
    {
        float lifetime = 140;
        float life = 0;
        Texture2D earthTex;
        float rotation;

        public Earth(float direction, Vector2 position, KingsOfAlchemy game, Player owner, float charge)
            : base(game, direction, 5, 500 * charge, 500 * (charge / 10), position, owner)
        {
            damage = 1;
            rotation = direction;
            earthTex = game.Content.Load<Texture2D>("rock");
            attackFixture.CollisionCategories = Category.Cat7;
            attackFixture.CollidesWith = Category.Cat1 | Category.Cat7;
            particleSystem.Add(new ParticleSystem(
                0,
                (float)Math.PI * 2,
                Position,
                new Vector2(0, 0.05f),
                new Vector2(0, 0.0000f),
                1,
                game.Content.Load<Texture2D>("Basicparticle"),
                -0.0001f,
                500,
                1000,
                Color.Brown));
            attackFixture.OnCollision += earthOnCollision;
        }
        public bool earthOnCollision(Fixture fix1, Fixture fix2, Contact con)
        {
            if (fix2.CollisionCategories == Category.Cat1)
            {
                if (life < (3 * lifetime / 4))
                {
                    life = 1 + (3 * lifetime / 4);
                }
            }
            return false;
        }
        public override void update(GameTime gameTime)
        {
            if (!Awake) return;
            life += 1;
            if (life > 3 * lifetime / 4)
            {
                attackFixture.CollisionCategories = Category.Cat6;
                attackFixture.CollidesWith = Category.Cat1 | Category.Cat2 | Category.Cat3 | Category.Cat5;
            }
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

        public override void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Awake) return;
            Vector2 Origin;
            Origin.X = earthTex.Width / 2;
            Origin.Y = earthTex.Height / 2;


            spriteBatch.Draw(earthTex,
                new Rectangle((int)Position.X + 10, (int)Position.Y + 10,
                50, 50),
                new Rectangle(0, 0, earthTex.Width, earthTex.Height),
                Color.White,
                rotation,
                Origin,
                SpriteEffects.None,
                0f);
            base.draw(gameTime, spriteBatch);
        }
    }
}
