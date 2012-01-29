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
        mainMenu,
        victoryScreen
    };
    public class KingsOfAlchemy : Microsoft.Xna.Framework.Game
    {
        SoundEffect menuSong;
        SoundEffect fightSong;

        SoundEffectInstance menuSongInstance;
        SoundEffectInstance fightSongInstance;

        int victoryScreenCounter = 0;
        int victoryScreenMaxCounter = 600;
        Player winner;


        public World world;
        public GameState gameState = GameState.mainMenu;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public List<ParticleSystem> particleSystems;
        public List<Attack> attacks;
        public Stage stage;


        //Menu stuff
        public bool lastPressed = false;
        public List<ParticleSystem> menuParticleSystems = new List<ParticleSystem>();
        public Texture2D ouru;
        public float ouruAngle = 0;
        public Texture2D name;
        public Texture2D bg;
        public Texture2D shield;
        public Vector2 ouruPos;

        public Texture2D stars;

        public List<Player> players;

        SpriteFont font;

        public List<Button> buttons = new List<Button>();
        public int selected = 0;

        public Random random;

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
            //particleSystems.Add(new ParticleSystem(0, 2 * (float)Math.PI, new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2), new Vector2(0.5f, 0), new Vector2(), 5, Content.Load<Texture2D>("1"), -0.001f, 500, 600));
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            random = new Random();
            // Create a new SpriteBatch, which can be used to draw textures.
            ouruPos = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height/4);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ouru = Content.Load<Texture2D>("BgOuru");
            bg = Content.Load<Texture2D>("avatar-floating-hills");
            name = Content.Load<Texture2D>("KingsOfAlchemy");
            stars = Content.Load<Texture2D>("StarsBG");
            shield = Content.Load<Texture2D>("Shield");
            font = Content.Load<SpriteFont>("SpriteFont1");
            menuSong = Content.Load<SoundEffect>("sounds/408038_orchestra");
            fightSong = Content.Load<SoundEffect>("sounds/432898_Beat_project_2");
            menuSongInstance = menuSong.CreateInstance();
            fightSongInstance = fightSong.CreateInstance();
            menuSongInstance.IsLooped = true;
            fightSongInstance.IsLooped = true;
            menuSongInstance.Play();

            buttons.Add(new startGameButton(this, 0, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height * 5 / 8)));
            buttons.Add(new exitButton(this, 1, new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height*7 / 8)));
            SoundManager.s.loadContent(this);
            //restartGame();
        }
        public void restartGame()
        {
            winner = null;
            world = new World(Vector2.Zero);
            particleSystems = new List<ParticleSystem>();
            attacks = new List<Attack>();
            players = new List<Player>();
            Vector2 offset = new Vector2(150, 0);

            //Initialize stage
            stage = new Stage(30, 30, this, offset);

            //Initialize players
            players = new List<Player>();
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex j;
                if(i == 0)
                    j = PlayerIndex.One;
                else if(i == 1)
                    j = PlayerIndex.Two;
                else if(i == 2)
                    j = PlayerIndex.Three;
                else
                    j = PlayerIndex.Four;
                if(GamePad.GetCapabilities(j).IsConnected)
                    players.Add(new Player(world, i + 1, this, offset));
            }


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (gameState == GameState.mainMenu){
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                {
                    this.Exit();
                }
            }
            
           
            if (gameState == GameState.fight)
            {
                world.Step(1);
                for (int i = 0; i < particleSystems.Count; ++i)
                {
                    if (particleSystems[i].particles.Count == 0 && particleSystems[i].destroyFlag)
                    {
                        particleSystems.RemoveAt(i);
                    }
                }
                foreach (Player p in players)
                {
                    p.Update();
                }
                foreach (Attack a in attacks)
                {
                    a.update(gameTime);
                }
                stage.update(gameTime);
                Player alivePlayer = null;
                bool gameOver = true;
                foreach (Player i in players)
                {
                    if (!i.dead)
                    {
                        if (alivePlayer == null)
                            alivePlayer = i;
                        else
                            gameOver = false;
                    }
                }
                if (gameOver)
                {
                    gameState = GameState.victoryScreen;
                    winner = alivePlayer;
                    SoundManager.s.playWin(winner.index);
                    victoryScreenCounter = 0;
                }
                
            }
            if (gameState == GameState.mainMenu)
            {
                foreach (Button b in buttons)
                {
                    b.step(gameTime);
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                gameState = GameState.mainMenu;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            SoundManager.s.update();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (gameState == GameState.fight || gameState == GameState.victoryScreen)
            {

                if (fightSongInstance.State != SoundState.Playing)
                {
                    fightSongInstance.Play();
                    menuSongInstance.Pause();
                }
                for (int i = 0; i < Window.ClientBounds.Width; i += stars.Width)
                {
                    for (int j = 0; j < Window.ClientBounds.Width; j += stars.Height)
                    {
                        spriteBatch.Draw(stars, new Rectangle(i * stars.Width, j * stars.Height, stars.Width, stars.Height), Color.White);
                    }
                }
                stage.draw(gameTime, spriteBatch);
                foreach (ParticleSystem i in particleSystems)
                {
                    i.draw(gameTime, spriteBatch);
                }
                foreach (Player p in players)
                {
                    p.draw(gameTime, spriteBatch);
                }
                foreach (Attack a in attacks)
                {
                    a.draw(gameTime, spriteBatch);
                }
            }
            if (gameState == GameState.victoryScreen)
            {
                victoryScreenCounter += 1;
                if (victoryScreenCounter > victoryScreenMaxCounter)
                    gameState = GameState.mainMenu;
                else
                {
                    spriteBatch.Draw(shield, new Rectangle((Window.ClientBounds.Width - shield.Width) / 2, (Window.ClientBounds.Height - shield.Height) / 2, shield.Width, shield.Height), Color.White);
                    spriteBatch.DrawString(font, "Player " + winner.index.ToString() + " Wins!", new Vector2(
                        Window.ClientBounds.Width/2- font.MeasureString("Player " + winner.index.ToString() + " Wins!").X/2,
                        Window.ClientBounds.Height/2- font.MeasureString("Player " + winner.index.ToString() + " Wins!").Y/2), Color.White);
                }

            }
            if (gameState == GameState.mainMenu)
            {
                if(fightSongInstance.State == SoundState.Playing){
                    fightSongInstance.Pause();
                    menuSongInstance.Play();
                }
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
               GamePadState state = GamePad.GetState(PlayerIndex.One);
               if (state.DPad.Down == ButtonState.Pressed || state.DPad.Up == ButtonState.Pressed || state.ThumbSticks.Left.Y != 0 || state.ThumbSticks.Right.Y != 0)
               {
                   if (!lastPressed)
                   {
                       selected += 1;
                       if (selected > 1)
                       {
                           selected = 0;
                       }
                   }

                   lastPressed = true;
               }
               else
               {
                   lastPressed = false;

               }

            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
