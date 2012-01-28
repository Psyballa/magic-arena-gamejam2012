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

namespace _2012Gamejam
{
    class Particle
    {
        Texture2D sprite;
        Vector2 velocity;
        Vector2 acceleration;
        Vector2 position;
        float transparency;
        float transparencyDelta;
        float rotation;
        float rotationSpeed;
        float lifetime;
        float time;

        public Particle(Texture2D sprite, Vector2 position, Vector2 velocity, Vector2 acceleration, float transparencyDelta, float rotationSpeed)
        {
            this.sprite = sprite;
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.transparencyDelta = transparencyDelta;
            this.rotationSpeed = rotationSpeed;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            velocity += acceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            rotation += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            transparency += transparencyDelta * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            spriteBatch.Draw(
                sprite,
                new Rectangle((int)position.X - sprite.Width / 2, (int)position.Y - sprite.Height / 2, sprite.Width, sprite.Height),
                new Rectangle(0, 0, sprite.Width, sprite.Height),
                Color.White * transparency,
                rotation,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0.01f);
        }
    }

    class ParticleSystem
    {
        Random random;
        float maxRot;
        float minRot;
        List<Particle> particles;

    }
}
