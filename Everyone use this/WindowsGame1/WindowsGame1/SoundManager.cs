using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio

namespace WindowsGame1
{
    class SoundManager
    {
        public static SoundManager s;
        bool playingVoice;
        SoundEffectInstance currVoice;

        // put sound variables here (make them public)

        private SoundManager()
        {
            //load sounds in here
        }

        // accepts a sound as an argument
        public void play ()
        {

        }

        public void playVoice( SoundEffect inSound)
        {
            if (!playingVoice)
            {
                currVoice = inSound.CreateInstance();
                playingVoice = true;
            }
        }

        public void update()
        {
            if(currVoice != null && currVoice.State != SoundState.Playing)
            {
                playingVoice = false;
                //currVoice.Dispose();  // do I need to do this?
            }
        }

    }
}
