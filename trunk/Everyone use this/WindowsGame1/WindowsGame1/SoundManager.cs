using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
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
    enum whoTalk
    {
        player1, player2, player3, player4, none
    }
    class SoundManager
    {
        public static SoundManager s = new SoundManager();
        public SoundEffectInstance currVoice;
        public List<SoundEffect> p1soundEffects;
        public List<SoundEffect> p2soundEffects;
        public List<SoundEffect> p3soundEffects;
        public List<SoundEffect> p4soundEffects;
        public whoTalk who = whoTalk.none;
        public Texture2D play1, play2, play3, play4;
        int talkTimer = 0;

        public List<SoundEffect> soundEffectQueue;

        Random random;

        int counter = 400;
        bool wasPlaying = false;

        private SoundManager()
        {
            random = new Random();
            //load sounds in here
        }
        public void loadPlayerSounds(List<SoundEffect> effectsList, String folderName, KingsOfAlchemy game)
        {
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "intro"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "water"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "fire"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "tornado"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "avalanche"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "win1"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "win2"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "hit1"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "hit2"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "hit3"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "damage1"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "damage2"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "damage3"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "joke"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "taunt"));
            effectsList.Add(game.Content.Load<SoundEffect>(folderName + "death"));

        }
        public void loadContent(KingsOfAlchemy game)
        {
            play1 = game.Content.Load<Texture2D>("PlayerSprites/player1.x1");
            play2 = game.Content.Load<Texture2D>("PlayerSprites/player2.x1");
            play3 = game.Content.Load<Texture2D>("PlayerSprites/player4.x1");
            play4 = game.Content.Load<Texture2D>("PlayerSprites/player5.x1");
            p1soundEffects = new List<SoundEffect>();
            p2soundEffects = new List<SoundEffect>();
            p3soundEffects = new List<SoundEffect>();
            p4soundEffects = new List<SoundEffect>();
            soundEffectQueue = new List<SoundEffect>();
            loadPlayerSounds(p1soundEffects, "sounds/P1/", game);
            loadPlayerSounds(p2soundEffects, "sounds/P2/", game);
            loadPlayerSounds(p3soundEffects, "sounds/P3/", game);
            loadPlayerSounds(p4soundEffects, "sounds/P4/", game);
            currVoice = p1soundEffects[0].CreateInstance();
        }

        public void attemptPlaySound(int index, int player)
        {
            if (random.NextDouble() > 0.2) return;
            if (currVoice.State != SoundState.Playing && soundEffectQueue.Count == 0 && counter > 40)
            {
                SoundEffect s;
                if(player == 1){
                    s = p1soundEffects[index];
                    who = whoTalk.player1;
                    talkTimer = 30;
                }
                else if(player == 2){
                    s = p2soundEffects[index];
                    who = whoTalk.player2;
                    talkTimer = 30;
                }
                else if (player == 3)
                {
                    s = p3soundEffects[index];
                    who = whoTalk.player3;
                    talkTimer = 30;
                }
                else
                {
                    s = p4soundEffects[index];
                    who = whoTalk.player4;
                    talkTimer = 30;
                }
                
                currVoice = s.CreateInstance();
                currVoice.Play();
            }
        }
        public void queueSound(int index, int player)
        {
            SoundEffect s;
            if (player == 1){
                s = p1soundEffects[index];
                who = whoTalk.player1;
                talkTimer = 30;
            }
            else if (player == 2){
                s = p2soundEffects[index];
                who = whoTalk.player2;
                talkTimer = 30;
            }
            else if (player == 3)
            {
                s = p3soundEffects[index];
                who = whoTalk.player3;
                talkTimer = 30;
            }
            else
            {
                s = p4soundEffects[index];
                who = whoTalk.player4;
                talkTimer = 30;
            }
            soundEffectQueue.Add(s);
        }

        public void playIntro(int player)
        {
            queueSound(0, player);
        }
        public void playWater(int player)
        {
            attemptPlaySound(1, player);
        }
        public void playFire(int player)
        {
            attemptPlaySound(2, player);
        }
        public void playAir(int player)
        {
            attemptPlaySound(3, player);
        }
        public void playEarth(int player)
        {
            attemptPlaySound(4, player);
        }
        public void playWin(int player)
        {
            queueSound(5 + (int)(2 * (random.NextDouble())), player);
        }
        public void playHit(int player)
        {
            attemptPlaySound(7 + (int)(3 * (random.NextDouble())), player);
        }
        public void playDamaged(int player)
        {
            attemptPlaySound(10 + (int)(3 * (random.NextDouble())), player);
        }
        public void playJoke(int player)
        {
            queueSound(13, player);
        }
        public void playTaunt(int player)
        {
            queueSound(14, player);
        }
        public void playDeath(int player)
        {
            attemptPlaySound(15, player);
        }


        public void update(GameTime gametime, SpriteBatch spriteBatch)
        {
            counter += 1;
            if (soundEffectQueue.Count > 0 && currVoice.State != SoundState.Playing && counter > 35)
            {
                currVoice = soundEffectQueue[0].CreateInstance();
                currVoice.Play();
                soundEffectQueue.RemoveAt(0);
            }
            if(currVoice.State == SoundState.Playing)
                wasPlaying = true;
            if (wasPlaying && currVoice.State != SoundState.Playing)
            {
                counter = 0;
                wasPlaying = false;
            }
            if (who != whoTalk.none)
            {
                talkTimer--;
                if (talkTimer <= 0)
                {
                    who = whoTalk.none;
                }
            }
            switch (who)
            {
                case whoTalk.player1:
                    spriteBatch.Draw(play1,
                    new Rectangle((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - play1.Width, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - play1.Height/2,
                    play1.Width, play1.Height),
                    new Rectangle(0, 0, play1.Width, play1.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0f);
                    break;
                case whoTalk.player2:
                    spriteBatch.Draw(play2,
                    new Rectangle((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - play2.Width, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - play1.Height / 2,
                    play1.Width, play1.Height),
                    new Rectangle(0, 0, play1.Width, play1.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0f);
                    break;
                case whoTalk.player3:
                    spriteBatch.Draw(play3,
                    new Rectangle((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - play1.Width, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - play1.Height / 2,
                    play1.Width, play1.Height),
                    new Rectangle(0, 0, play1.Width, play1.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0f);
                    break;
                case whoTalk.player4:
                    spriteBatch.Draw(play4,
                    new Rectangle((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - play1.Width, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - play1.Height / 2,
                    play1.Width, play1.Height),
                    new Rectangle(0, 0, play1.Width, play1.Height),
                    Color.White,
                    0,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0f);
                    break;
                default:
                    break;
            }
        }
    }
}
