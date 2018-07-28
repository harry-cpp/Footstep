using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Footstep
{
    class Camera
    {
        private Vector3 _position;
        private Vector3 _up = Vector3.UnitZ;
        private Vector3 _forward = Vector3.Up;
        private float _fieldOfView = (float)Math.PI / 4;

        private Matrix _projection;
        private Matrix _view;
        private Matrix _world;

        private float _speed = 3f;
        private float _rotationSpeed = 0.002f;

        private static MouseState _prevMState, _mState;

        public Camera(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            _position = new Vector3(Utility.GameMap.Spawn.X + 0.5f, Utility.GameMap.Spawn.Y + 0.5f, 0.25f);

            updateMatrix();
        }

        public GraphicsDeviceManager Graphics { get; private set; }

        public Matrix Projection => _projection;

        public Matrix View => _view;

        public Matrix World => _world;

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }
        
        public Vector3 Up
        {
            get { return _up; }
            set
            {
                _up = value;
            }
        }

        public Vector3 Forward
        {
            get { return _forward; }
            set
            {
                _forward = value;
            }
        }

        public float FieldOfView
        {
            get { return _fieldOfView; }
            set
            {
                _fieldOfView = value;
            }
        }

        public Vector3 Lookat
        {
            get { return _position + _forward;}
            set
            {
                Forward = value - Position;
                _forward.Normalize();
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;

            _prevMState = _mState;
            _mState = Mouse.GetState();

            if (Utility.Game.IsActive)
            {
                Vector3 normal = Vector3.Cross(Forward, Up);

                if (_mState != _prevMState)
                {
                    float dx = _mState.X - _prevMState.X;
                    float dy = _mState.Y - _prevMState.Y;

                    Mouse.SetPosition(Graphics.GraphicsDevice.Viewport.Width / 2, Graphics.GraphicsDevice.Viewport.Height / 2);
                    _mState = Mouse.GetState();

                    Forward += dx * _rotationSpeed * normal;
                    //Forward -= dy * _rotationSpeed * Up;
                    _forward.Normalize();
                }

                if (InputManager.IsDown(InputKey.MoveForward))
                    Position += Forward * _speed * deltaTime;

                if (InputManager.IsDown(InputKey.MoveBackwards))
                    Position -= Forward * _speed * deltaTime;

                if (InputManager.IsDown(InputKey.MoveLeft))
                    Position -= normal * _speed * deltaTime;

                if (InputManager.IsDown(InputKey.MoveRight))
                    Position += normal * _speed * deltaTime;
            }

            updateMatrix();
        }

        private void updateMatrix()
        {
            _view = Matrix.CreateLookAt(Position, Lookat, Up);

            _projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, Graphics.PreferredBackBufferWidth * 1f / Graphics.PreferredBackBufferHeight, 0.1f, 100f);

            _world = Matrix.Identity;
        }
    }
}