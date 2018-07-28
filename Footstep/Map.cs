namespace Footstep
{
    class Map
    {
        private Tiles[,] _grid;

        public Map()
        {
            _grid = new Tiles[5, 5] {
                {Tiles.Hallway, Tiles.None, Tiles.Hallway, Tiles.None, Tiles.None},
                {Tiles.Hallway, Tiles.None, Tiles.Hallway, Tiles.None, Tiles.None},
                {Tiles.Hallway, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway, Tiles.Hallway},
                {Tiles.None, Tiles.None, Tiles.None, Tiles.None, Tiles.None},
                {Tiles.None, Tiles.None, Tiles.None, Tiles.None, Tiles.None},
            };
        }

        public Tiles[,] Grid => _grid;
    }
}