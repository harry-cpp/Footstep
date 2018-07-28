using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class Floor : IGameObject
    {
        private Texture2D _texture;
        private VertexPositionNormalTexture[] _vertices;

        public Floor()
        {
            _vertices = new VertexPositionNormalTexture[6];

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
            _vertices[5].TextureCoordinate = _vertices[2].TextureCoordinate;
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

                camera.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, 2);
            }
        }
    }
}