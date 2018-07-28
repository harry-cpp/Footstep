using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class Floor : IGameObject
    {
        private Texture2D _texture;
        private VertexPositionNormalTexture[] _vertices;

        struct TileLocation
        {
            public int X;
            public int Y;
            public Tiles Tile;
        }

        public Floor()
        {
            /*_vertices = new VertexPositionNormalTexture[6];

            _vertices[0].Position = new Vector3(-20, -20, 0);
            _vertices[1].Position = new Vector3(-20, 20, 0);
            _vertices[2].Position = new Vector3(20, -20, 0);

            _vertices[3].Position = _vertices[1].Position;
            _vertices[4].Position = new Vector3(20, 20, 0);
            _vertices[5].Position = _vertices[2].Position;

            int repetition = 40;

            _vertices[0].TextureCoordinate = new Vector2(0, 0);
            _vertices[1].TextureCoordinate = new Vector2(0, repetition);
            _vertices[2].TextureCoordinate = new Vector2(repetition, 0);

            _vertices[3].TextureCoordinate = _vertices[1].TextureCoordinate;
            _vertices[4].TextureCoordinate = new Vector2(repetition, repetition);
            _vertices[5].TextureCoordinate = _vertices[2].TextureCoordinate;*/

            generateFloor();
        }

        private void generateFloor()
        {
            List<TileLocation> tiles = new List<TileLocation>();
            for (int i = 0; i < Utility.GameMap.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Utility.GameMap.Grid.GetLength(1); j++)
                {
                    if (Utility.GameMap.Grid[i, j] != Tiles.None) {
                        tiles.Add(new TileLocation{ X = i, Y = j, Tile = Utility.GameMap.Grid[i, j]});
                    }
                }
            }

            _vertices = new VertexPositionNormalTexture[tiles.Count * 6];
            int index = 0;

            foreach (TileLocation tl in tiles)
            {
                _vertices[index + 0].Position = new Vector3(tl.X, tl.Y, 0);
                _vertices[index + 1].Position = new Vector3(tl.X, tl.Y + 1, 0);
                _vertices[index + 2].Position = new Vector3(tl.X + 1, tl.Y, 0);

                _vertices[index + 3].Position = _vertices[index + 1].Position;
                _vertices[index + 4].Position = new Vector3(tl.X + 1, tl.Y + 1, 0);
                _vertices[index + 5].Position = _vertices[index + 2].Position;

                _vertices[index + 0].TextureCoordinate = new Vector2(0, 0);
                _vertices[index + 1].TextureCoordinate = new Vector2(0, 1);
                _vertices[index + 2].TextureCoordinate = new Vector2(1, 0);

                _vertices[index + 3].TextureCoordinate = _vertices[index + 1].TextureCoordinate;
                _vertices[index + 4].TextureCoordinate = new Vector2(1, 1);
                _vertices[index + 5].TextureCoordinate = _vertices[index + 2].TextureCoordinate;

                index += 6;
            }
        }

        public override void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _texture = content.Load<Texture2D>("Floor");
        }

        public override void Draw(GameTime gameTime, Camera camera)
        {
            Utility.Effect.Texture = _texture;

            foreach (var pass in Utility.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                camera.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, _vertices.Length / 3);
            }
        }
    }
}