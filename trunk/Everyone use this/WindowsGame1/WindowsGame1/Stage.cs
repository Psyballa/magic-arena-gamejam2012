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
    public class Stage
    {
        Tile[,] tiles;
        List<Wall> walls;

        public int height
        {
            get
            {
                return tiles.GetLength(0);
            }
        }
        public int width
        {
            get
            {
                return tiles.GetLength(1);
            }
        }

        // have arrays work with stuff, just need to handle creating and updating the arrays of them.

        public Stage(int width, int height, KingsOfAlchemy game){

            //Populate arrays
            tiles = new Tile[width,height];
            walls = new List<Wall>();
            for(int i = 0; i < width; ++i){
                for(int j = 0; j < height; ++j){
                    if(i == 0 || j == 0 || i == width - 1 || j == height-1){
                        walls.Add(new Wall(game.world, new Microsoft.Xna.Framework.Vector2(i, j), game));
                    }
                    else{
                        tiles[i,j] = new Tile(game.world, new Microsoft.Xna.Framework.Vector2(i, j), game);
                    }
                }
            }
        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Wall w in walls)
            {
                w.draw(gameTime, spriteBatch);
            }
            foreach (Tile t in tiles)
            {
                if (t != null)
                    t.draw(gameTime, spriteBatch);
            }

        }
        public void update(GameTime gameTime)
        {
            foreach (Wall w in walls)
            {
                w.Update();
            }
            foreach (Tile t in tiles)
            {
                if (t != null)
                    t.Update();
            }

        }
    }
}
