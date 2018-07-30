using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class Wall : IGameObject
    {
        private Texture2D _texture;
        private VertexPositionNormalTexture[] _vertices;

        struct WallLocation
        {
            public int X1;
            public int Y1;
            public int X2;
            public int Y2;
            public TileType Tile;
        }

        public Wall()
        {
            generateWall();
        }
        
        private void generateWall()
        {
            List<WallLocation> tiles = new List<WallLocation>();
            for (int i = 0; i < Utility.GameMap.Grid.GetLength(0); i++)
            {
                bool lastWasTile = false;
                TileType currentTile = TileType.None;
                for (int j = 0; j < Utility.GameMap.Grid.GetLength(1); j++)
                {
                    currentTile = Utility.GameMap.Grid[i, j].Type;
                    if (!lastWasTile && currentTile != TileType.None)
                    {
                        tiles.Add(new WallLocation{ X1 = i, Y1 = j, X2 = i + 1, Y2 = j, Tile = currentTile});
                    } else if (lastWasTile && currentTile == TileType.None)
                    {
                        tiles.Add(new WallLocation{ X1 = i + 1, Y1 = j, X2 = i, Y2 = j, Tile = currentTile});
                    }
                    
                    if (currentTile != TileType.None)
                    {
                        lastWasTile = true;
                    } else {
                        lastWasTile = false;
                    }
                }
                if (lastWasTile)
                {
                    int y = Utility.GameMap.Grid.GetLength(1);
                    tiles.Add(new WallLocation{ X1 = i + 1, Y1 = y, X2 = i, Y2 = y, Tile = currentTile});
                }
            }
            for (int j = 0; j < Utility.GameMap.Grid.GetLength(1); j++)
            {
                bool lastWasTile = false;
                TileType currentTile = TileType.None;
                for (int i = 0; i < Utility.GameMap.Grid.GetLength(0); i++)
                {
                    currentTile = Utility.GameMap.Grid[i, j].Type;
                    if (!lastWasTile && currentTile != TileType.None)
                    {
                        tiles.Add(new WallLocation{ X1 = i, Y1 = j + 1, X2 = i, Y2 = j, Tile = currentTile});
                    } else if (lastWasTile && currentTile == TileType.None)
                    {
                        tiles.Add(new WallLocation{ X1 = i, Y1 = j, X2 = i, Y2 = j + 1, Tile = currentTile});
                    }
                    
                    if (currentTile != TileType.None)
                    {
                        lastWasTile = true;
                    } else {
                        lastWasTile = false;
                    }
                }
                if (lastWasTile)
                {
                    int x = Utility.GameMap.Grid.GetLength(0);
                    tiles.Add(new WallLocation{ X1 = x, Y1 = j, X2 = x, Y2 = j + 1, Tile = currentTile});
                }
            }

            _vertices = new VertexPositionNormalTexture[tiles.Count * 6];
            int index = 0;

            foreach (WallLocation tl in tiles)
            {
                _vertices[index + 0].Position = new Vector3(tl.X1, tl.Y1, 1);
                _vertices[index + 1].Position = new Vector3(tl.X1, tl.Y1, 0);
                _vertices[index + 2].Position = new Vector3(tl.X2, tl.Y2, 1);

                _vertices[index + 3].Position = _vertices[index + 1].Position;
                _vertices[index + 4].Position = new Vector3(tl.X2, tl.Y2, 0);
                _vertices[index + 5].Position = _vertices[index + 2].Position;

                _vertices[index + 0].TextureCoordinate = new Vector2(1, 0);
                _vertices[index + 1].TextureCoordinate = new Vector2(1, 1);
                _vertices[index + 2].TextureCoordinate = new Vector2(0, 0);

                _vertices[index + 3].TextureCoordinate = _vertices[index + 1].TextureCoordinate;
                _vertices[index + 4].TextureCoordinate = new Vector2(0, 1);
                _vertices[index + 5].TextureCoordinate = _vertices[index + 2].TextureCoordinate;

                index += 6;
            }
        }

        public override void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _texture = content.Load<Texture2D>(GameContent.Texture.Wall);
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            if (_vertices.Length > 0) {
                Utility.Effect.Texture = _texture;

                foreach (var pass in Utility.Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    camera.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length / 3);
                }
            }
        }
    }
}