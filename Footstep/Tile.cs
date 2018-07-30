using System;

namespace Footstep
{
    class Tile
    {
        private int _x;
        private int _y;
        private TileType _type;

        public Tile(int iX, int iY, TileType type)
        {
            _x = iX;
            _y = iY;
            _type = type;
        }

        public int X => _x;
        public int Y => _y;
        public TileType Type => _type;
    }
}