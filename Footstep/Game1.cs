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
            _graphics.PreferredBackBufferWidth = 320 * 4;
            _graphics.PreferredBackBufferHeight = 200 * 4;

            IsFixedTimeStep = false;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            Window.Title = "Monogame 3D Tests";
            IsMouseVisible = false;

            //GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Utility.Game = this;
            Utility.Effect = new BasicEffect(_graphics.GraphicsDevice);
            Utility.Window = Window;

            Utility.GameMap = MapGenerator.GenerateMap();
            _camera = new Camera(_graphics);
            _objects = new List<IGameObject>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            var floor = new Floor();
            var wall = new Wall();
            var ceiling = new Ceiling();
            _objects.Add(floor);
            _objects.Add(wall);
            _objects.Add(ceiling);

            foreach(var obj in _objects)
                obj.Load(Content, _graphics);

            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();

            _camera.Update(gameTime);
            foreach (var obj in _objects)
                obj.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Utility.Effect.World = _camera.World;
            Utility.Effect.View = _camera.View;
            Utility.Effect.Projection = _camera.Projection;

            Utility.Effect.TextureEnabled = true;

            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach(var obj in _objects)
                obj.Draw(gameTime, _camera);

            base.Draw(gameTime);
        }
    }
}