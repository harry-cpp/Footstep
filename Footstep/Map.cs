using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Footstep
{
    class Map
    {
        private Tile[,] _grid;
        private Vector2 _spawn;
        private int _width;
        private int _height;
        struct TileValue
        {
            public Tile tile;
            public int value;
            public TileValue(Tile t, int v)
            {
                tile = t;
                value = v;
            }
        }

        public Map()
        {
            /*Tile hallway = new Tile(1, 1, TileType.Hallway);
            Tile none = new Tile(1, 1, TileType.None);
            _grid = new Tile[5, 5] {
                {none, hallway, none, none, none},
                {none, hallway, hallway, none, none},
                {hallway, hallway, hallway, hallway, hallway},
                {none, hallway, none, none, none},
                {none, none, none, none, none},
            };*/

            _width = 100;
            _height = 100;

            _grid = new Tile[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _grid[i, j] = new Tile(i, j, TileType.None);
                }
            }

            generateMap();
        }

        public Tile[,] Grid
        {
            get => _grid;
            set
            {
                _grid = value;
            }
        }

        public Vector2 Spawn
        {
            get => _spawn;
            set
            {
                _spawn = value;
            }
        }

        public int Width => _width;

        public int Height => _height;

        private void generateMap()
        {
            List<Tile> path = new List<Tile>();
            int length = 0;
            int maxLength = 500;

            int startX = Utility.Random.Next(0, Width);
            int startY = Utility.Random.Next(0, Height);

            Spawn = new Vector2(startX, startY);
            Tile startTile = _grid[startX, startY];
            Tile t = startTile;

            path.Add(t);
            length++;

            do
            {
                HashSet<Tile> nextTiles = FindNeighbors(t);
                List<TileValue> nextTileSort = new List<TileValue>();
                foreach (Tile i in nextTiles)
                {
                    nextTileSort.Add(new TileValue(i, GetTileValue(i, startTile, path)));
                }
                nextTileSort.Sort((v1, v2) => v1.value.CompareTo(v2.value));
                t = nextTileSort.Last().tile;
                path.Add(t);

                length++;
            } while (length < maxLength);

            foreach (Tile i in path)
            {
                _grid[i.X, i.Y].Type = TileType.Hallway;
            }
        }

        private int GetTileValue(Tile t, Tile start, List<Tile> p) {
            int tileValue = 0;

            Vector2 relativeCenter = new Vector2(0.5f, 0.5f);
            Vector2 relativeStart = new Vector2((start.X + 0.5f) / Width, (start.Y + 0.5f) / Height);
            Vector2 relativeMapping = new Vector2((t.X + 0.5f) / Width, (t.Y + 0.5f) / Height);

            int centerDistance = (int)(Vector2.Distance(relativeCenter, relativeMapping) * 100f);
            int startDistance = (int)(Vector2.Distance(relativeStart, relativeMapping) * 50f);

            tileValue += (100 - centerDistance) * 3 + Utility.Random.Next(0, 100);

            if (p.Contains(t)) {
                tileValue -= 9999;
            }

            tileValue -= 75 - startDistance;

            return tileValue;
        }

        private HashSet<Tile> FindNeighbors(Tile t)
        {
            HashSet<Tile> neighbors = new HashSet<Tile>();
            if (t.X - 1 >= 0)
            {
                neighbors.Add(_grid[t.X - 1, t.Y]);
            }
            if (t.X + 1 < Width)
            {
                neighbors.Add(_grid[t.X + 1, t.Y]);
            }
            if (t.Y - 1 >= 0)
            {
                neighbors.Add(_grid[t.X, t.Y - 1]);
            }
            if (t.Y + 1 < Height)
            {
                neighbors.Add(_grid[t.X, t.Y + 1]);
            }

            return neighbors;
        }
    }
}