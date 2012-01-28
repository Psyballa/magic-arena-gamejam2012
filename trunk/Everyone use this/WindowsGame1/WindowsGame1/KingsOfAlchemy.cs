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
    public enum GameState{
        fight,
        mainMenu
    };
    public class KingsOfAlchemy : Microsoft.Xna.Framework.Game
    {
        public World world;
        public GameState gameState = GameState.mainMenu;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        //Menu stuff
        List<ParticleSystem> menuParticleSystems = new List<ParticleSystem>();
        Texture2D ouru;
        float ouruAngle = 0;
        Texture2D name;
        Texture2D bg;
        Vector2 ouruPos;

        Stage stage;

        List<Button> buttons = new List<Button>();
        public int selected = 0;

        public KingsOfAlchemy()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World(Vector2.UnitY);
            //particleSystems.Add(new ParticleSystem(0, 2 * (float)Math.PI, new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2), new Vector2(0.5f, 0), new Vector2(), 5, Content.Load<Texture2D>("1"), -0.001f, 500, 600));
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            ouruPos = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height/4);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ouru = Content.Load<Texture2D>("BgOuru");
            bg = Content.Load<Texture2D>("avatar-floating-hills");
            name = Content.Load<Texture2D>("KingsOfAlchemy");
            buttons.Add(new startGameButton(this, 0, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height * 5 / 8)));
            buttons.Add(new exitButton(this, 0, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height*7 / 8)));


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (gameState == GameState.fight)
            {
                for (int i = 0; i < particleSystems.Count; ++i)
                {
                    if (particleSystems[i].particles.Count == 0 && particleSystems[i].destroyFlag)
                    {
                        particleSystems.RemoveAt(i);
                    }
                }
            }
            if (gameState == GameState.mainMenu)
            {
                foreach(Button b in buttons){
                    b.step(gameTime);
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (gameState == GameState.fight)
            {
                foreach (ParticleSystem i in particleSystems)
                {
                    i.draw(gameTime, spriteBatch);
                }
            }
            if (gameState == GameState.mainMenu)
            {
                foreach (ParticleSystem i in menuParticleSystems)
                {
                    i.draw(gameTime, spriteBatch);
                }
                ouruAngle += 0.01f;
                spriteBatch.Draw(bg, new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
                spriteBatch.Draw(ouru, ouruPos, ouru.Bounds, Color.White, ouruAngle, new Vector2(ouru.Bounds.Center.X, ouru.Bounds.Center.Y), new Vector2(0.3f, 0.3f), SpriteEffects.FlipHorizontally, 0.5f);
                spriteBatch.Draw(name, ouruPos, name.Bounds, Color.White, 0, new Vector2(name.Bounds.Center.X, name.Bounds.Center.Y), new Vector2(1, 1), SpriteEffects.None, 0.5f);
                foreach (Button b in buttons)
                {
                    b.draw(gameTime, spriteBatch);
                }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
