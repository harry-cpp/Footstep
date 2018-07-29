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
        private SpriteBatch _spriteBatch;
        private RenderTarget2D _renderTarget;

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
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            IsMouseVisible = false;

            //GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Utility.Game = this;
            Utility.Effect = new BasicEffect(_graphics.GraphicsDevice);
            Utility.Window = Window;

            Utility.GameMap = MapGenerator.GenerateMap();
            _camera = new Camera(_graphics);
            _objects = new List<IGameObject>();

            Window_ClientSizeChanged(null, EventArgs.Empty);

            base.Initialize();
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            GameSettings.Width = _graphics.GraphicsDevice.Viewport.Width;
            GameSettings.Height = _graphics.GraphicsDevice.Viewport.Height;

            GameSettings.RenderTargetHeight = 480;
            GameSettings.RenderTargetWidth = (int)(((float)(GameSettings.RenderTargetHeight * GameSettings.Width)) / GameSettings.Height);

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, GameSettings.RenderTargetWidth, GameSettings.RenderTargetHeight);

            foreach (var obj in _objects)
                obj.SizeChanged();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, GameSettings.RenderTargetWidth, GameSettings.RenderTargetHeight);

            _objects.Add(new Floor());
            _objects.Add(new Wall());
            _objects.Add(new Ceiling());
            _objects.Add(new GameUI());

            foreach(var obj in _objects)
            {
                obj.Load(Content, _graphics);
                obj.SizeChanged();
            }

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
            // Draw UI Layer
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin();
            foreach(var obj in _objects)
                obj.DrawUI(gameTime, _spriteBatch);
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            // Render game
            Utility.Effect.World = _camera.World;
            Utility.Effect.View = _camera.View;
            Utility.Effect.Projection = _camera.Projection;
            Utility.Effect.TextureEnabled = true;

            foreach(var obj in _objects)
                obj.Draw(gameTime, _camera);

            // Render UI
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, GameSettings.Width, GameSettings.Height), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}