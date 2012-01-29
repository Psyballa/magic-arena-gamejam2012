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
        Vector2 offset;

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

        public Stage(int width, int height, KingsOfAlchemy game, Vector2 offset){
            Random random = new Random();
            //Populate arrays
            tiles = new Tile[width,height];
            walls = new List<Wall>();
            for(int i = 0; i < width; ++i){
                for(int j = 0; j < height; ++j){
                    if(i == 0 || j == 0 || i == width - 1 || j == height-1){
                        walls.Add(new Wall(game.world, new Microsoft.Xna.Framework.Vector2(i, j), game, offset));
                    }
                    else{
                        tiles[i,j] = new Tile(game.world, new Microsoft.Xna.Framework.Vector2(i, j), game, offset);
                    }
                }
            }
            if (random.NextDouble() > 0.4)
            {
                for (int i = height / 4; i < height * 3 / 4; ++i)
                    walls.Add(new Wall(game.world, new Vector2(width / 2, i), game, offset));
                for (int i = width / 4; i < width * 3 / 4; ++i)
                    walls.Add(new Wall(game.world, new Vector2(i, height / 2), game, offset));
            }
            if (random.NextDouble() > 0.3)
            {
                for (int i = height/4; i < height*3/4; ++i)
                    walls.Add(new Wall(game.world, new Vector2(i, i), game, offset));
                for (int i = width/4; i < width*3/4; ++i)
                    walls.Add(new Wall(game.world, new Vector2(i, height - i - 1), game, offset));
            }
            if (random.NextDouble() > 0.6)
            {
                for (int i = 0; i < walls.Count; i += 2)
                {
                    walls.RemoveAt(i);
                }
            }
            if (random.NextDouble() > 0.3)
            {
                for (int i = height * 3 / 8 - 3; i < height * 5 / 8 + 3; ++i)
                {
                    for (int j = width * 3 / 8 - 3; j < width * 5 / 8 + 3; ++j)
                    {
                        tiles[i, j] = null;
                    }
                }
            }





        }
        public void draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Tile t in tiles)
            {
                if (t != null)
                    t.draw(gameTime, spriteBatch);
            }
            foreach (Wall w in walls)
            {
                w.draw(gameTime, spriteBatch);
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
