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
    public class Particle
    {
        public Texture2D sprite;
        public Vector2 velocity;
        public Vector2 acceleration;
        public Vector2 position;
        float transparency = 1.0f;
        float transparencyDelta;
        float rotation;
        float rotationSpeed;
        public float lifetime;
        public float time = 0;
        Color color;

        public Particle(Texture2D sprite, 
            Vector2 position, 
            Vector2 velocity, 
            Vector2 acceleration, 
            float transparencyDelta, 
            float rotationSpeed, 
            float lifetime,
            Color color)
        {
            this.sprite = sprite;
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.transparencyDelta = transparencyDelta;
            this.rotationSpeed = rotationSpeed;
            this.time = 0;
            this.lifetime = lifetime;
            this.color = color;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            velocity += acceleration * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            rotation += rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            transparency += transparencyDelta * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            spriteBatch.Draw(
                sprite,
                new Rectangle((int)position.X - sprite.Width / 2, (int)position.Y - sprite.Height / 2, sprite.Width, sprite.Height),
                new Rectangle(0, 0, sprite.Width, sprite.Height),
                color * transparency,
                rotation,
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None,
                0.01f);
        }
    }

    public class ParticleSystem
    {
        Random random;
        float maxRot;
        float minRot;

        public Vector2 position;

        Vector2 initVelocity;
        Vector2 initAccel;

        public List<Particle> particles = new List<Particle>();
        int add;
        Texture2D sprite;
        float transDelta;
        int minlife;
        int maxlife;
        public bool destroyFlag = false;
        Color color;
        public void destroy()
        {
            add = 0;
            destroyFlag = true;
        }

        public ParticleSystem(float minRot,
            float maxRot,
            Vector2 position,
            Vector2 initVelocity,
            Vector2 initAccel,
            int add,
            Texture2D sprite,
            float transDelta,
            int minlife,
            int maxlife)
        {
            this.minRot = minRot;
            this.maxRot = maxRot;
            this.position = position;
            this.initVelocity = initVelocity;
            this.initAccel = initAccel;
            this.add = add;
            this.sprite = sprite;
            this.transDelta = transDelta;
            this.minlife = minlife;
            this.maxlife = maxlife;
            this.random = new Random();
            this.color = Color.White;
        }
        public ParticleSystem(float minRot,
            float maxRot,
            Vector2 position,
            Vector2 initVelocity,
            Vector2 initAccel,
            int add,
            Texture2D sprite,
            float transDelta,
            int minlife,
            int maxlife, 
            Color color)
        {
            this.minRot = minRot;
            this.maxRot = maxRot;
            this.position = position;
            this.initVelocity = initVelocity;
            this.initAccel = initAccel;
            this.add = add;
            this.sprite = sprite;
            this.transDelta = transDelta;
            this.minlife = minlife;
            this.maxlife = maxlife;
            this.random = new Random();
            this.color = color;
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Particle i in particles)
            {
                i.Draw(gameTime, spriteBatch);
            }
            for (int i = 0; i < particles.Count; ++i)
            {
                if (particles[i].time > particles[i].lifetime)
                {
                    particles.RemoveAt(i);
                    --i;
                }
            }
            for (int i = 0; i < add; ++i)
            {
                float rotspeed =  (float)random.NextDouble() * (maxRot - minRot) + minRot;
                int lifetime = (int)(random.NextDouble() * (maxlife - minlife) + minlife);
                float angle = (float)random.NextDouble() * (maxRot - minRot) + minRot;
                Vector2 velocity = Vector2.Transform(initVelocity, Matrix.CreateRotationZ(angle));
                Vector2 acceleration = Vector2.Transform(initAccel, Matrix.CreateRotationZ(angle));

                particles.Add(new Particle(sprite, position, velocity, acceleration, transDelta,0, lifetime, color));
            }
        }
    }
}
