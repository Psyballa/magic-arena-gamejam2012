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
    public abstract class Button
    {
        public Texture2D sprite;
        public Vector2 position;
        public bool prevClicked = false;
        public Game1 game;

        public int index;

        public abstract void click();

        public void step(GameTime gameTime)
        {
            if (game.selected == index && GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && !prevClicked)
            {
                click();
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.A != ButtonState.Pressed)
                {
                    prevClicked = false;
                }
            }
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                new Rectangle(
                    (int)position.X - sprite.Width / 2,
                    (int)position.Y - sprite.Height / 2,
                    sprite.Width,
                    sprite.Height),
                    Color.White);
        }

    }
    public class startGameButton : Button
    {
        public startGameButton(Game1 game, int index, Vector2 position)
        {
            this.position = position;
            this.game = game;
            this.sprite = game.Content.Load<Texture2D>("Start");
            this.index = index;
        }
        public override void click()
        {
            game.gameState = GameState.fight;
        }
    }
    public class exitButton : Button
    {
        public exitButton(Game1 game, int index, Vector2 position)
        {
            this.position = position;
            this.game = game;
            this.sprite = game.Content.Load<Texture2D>("Exit");
            this.index = index;
        }
        public override void click()
        {
            game.Exit();
        }
    }
}
