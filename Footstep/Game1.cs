using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Footstep
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Camera _camera;
        private List<IGameObject> _objects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 600;
        }

        protected override void Initialize()
        {
            Window.Title = "Monogame 3D Tests";
            IsMouseVisible = true;

            _camera = new Camera(_graphics);
            _objects = new List<IGameObject>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            var floor = new Floor();
            _objects.Add(floor);

            foreach(var obj in _objects)
                obj.Load(Content, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _camera.Update(gameTime);
            foreach (var obj in _objects)
                obj.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(var obj in _objects)
                obj.Draw(gameTime, _camera);

            base.Draw(gameTime);
        }
    }
}