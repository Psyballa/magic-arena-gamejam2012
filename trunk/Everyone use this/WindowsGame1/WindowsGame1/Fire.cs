using System;
using System.Collections.Generic;
using System.Linq;
//using System.Math;
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
    class Fire : Attack
    {
        public const int MIN_FIRE_RANGE = 50;
        public const int MAX_FIRE_RANGE = 100;
        public const float MIN_FIRE_SPREAD = (float) Math.PI / 2;
        public const float MAX_FIRE_SPREAD = (float) Math.PI * 2;
        public const int MIN_FIRE_SHOTS = 3;
        public const int MAX_FIRE_SHOTS = 36;



        public Fire(KingsOfAlchemy gameWorld, float dmg, float rot, float density, float speed, Vector2 p, Player owner)
            :base(gameWorld, dmg, 2 ,density, speed, p, owner)
        {
            attackFixture = FixtureFactory.AttachRectangle(radius, 3.0f, density, new Vector2(0,0), this);
            this.Rotation = rot;
        }

        public virtual void update()
        {
            //grow rectangle larger
            // fixture.width += ...;
            
        }
    }
}
