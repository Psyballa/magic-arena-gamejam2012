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
    class Floor : Body
    {
        int health = 30;
        Vector2 position;
        Texture2D sprite;
        float rotation;
        
        public Floor(Vector2 position, float rotation, Texture2D sprite){
            this.position = position;
            this.rotation = rotation;
            this.sprite = sprite;
        }

    }

    class Wall : Fixture
    {
        int health;
        Vector2 position;
        Texture2D sprite;
        float rotation;
        public Wall(Vector2 position, float rotation, Texture2D sprite){
            this.position = position;
            this.rotation = rotation;
            this.sprite = sprite;
        }


    }
}
