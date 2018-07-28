using System;
using Microsoft.Xna.Framework;

namespace Footstep
{
    static class MapGenerator
    {
        public static Map GenerateMap()
        {
            Map map = new Map();

            int width = 100;
            int height = 100;

            Tiles[,] grid = new Tiles[width, height];

            Random random = new Random();

            int startX = random.Next(0, width);
            int startY = random.Next(0, height);

            map.Spawn = new Vector2(startX, startY);

            grid = growRoom(grid, startX, startY);

            map.Grid = grid;
            return map;
        }

        private static Tiles[,] growRoom(Tiles[,] grid, int x, int y)
        {
            int roomSizeX = 3;
            int roomSizeY = 3;

            for (int i = x; i < Math.Min(grid.GetLength(0), x + roomSizeX); i++)
            {
                for (int j = y; j < Math.Min(grid.GetLength(1), y + roomSizeY); j++)
                {
                    grid[i, j] = Tiles.Hallway;
                }
            }

            return grid;
        }
    }
}