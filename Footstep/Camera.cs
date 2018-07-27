using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Footstep
{
    class Camera
    {
        private Vector3 _position;
        private Matrix _projection;
        private Matrix _view;
        private Matrix _world;

        public Camera(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            _position = Vector3.Zero;

            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), graphics.PreferredBackBufferWidth * 1f / graphics.PreferredBackBufferHeight, 0.1f, 100f);
            _view = Matrix.CreateLookAt(new Vector3(0, 0, 2), new Vector3(0, 2, 2), Vector3.UnitZ);
            _world = Matrix.CreateTranslation(Vector3.Zero);
        }

        public GraphicsDeviceManager Graphics { get; private set; }

        public Matrix Projection => _projection;

        public Matrix View => _view;

        public Matrix World => _world;

        public void Update(GameTime gameTime)
        {
            if (InputManager.IsDown(InputKey.MoveForward))
                _position.Y -= 0.1f;

            if (InputManager.IsDown(InputKey.MoveBackwards))
                _position.Y += 0.1f;

            if (InputManager.IsDown(InputKey.MoveLeft))
                _position.X += 0.1f;

            if (InputManager.IsDown(InputKey.MoveRight))
                _position.X -= 0.1f;

            _world = Matrix.CreateTranslation(_position);
        }
    }
}