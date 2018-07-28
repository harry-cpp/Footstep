using Microsoft.Xna.Framework;

namespace Footstep
{
    class Map
    {
        private Tiles[,] _grid;
        private Vector2 _spawn;

        public Map()
        {
            _grid = new Tiles[5, 5] {
                {Tiles.Hallway, Tiles.None, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway},
                {Tiles.Hallway, Tiles.None, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway},
                {Tiles.Hallway, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway},
                {Tiles.None, Tiles.None, Tiles.None, Tiles.None, Tiles.Hallway},
                {Tiles.None, Tiles.None, Tiles.None, Tiles.None, Tiles.Hallway},
            };
        }

        public Tiles[,] Grid
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
    }
}