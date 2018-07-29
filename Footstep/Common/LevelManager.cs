using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    static class LevelManager
    {
        public static bool UpdateCamera { get; set; }

        private static ILevel _level;
        private static GraphicsDeviceManager _graphics;
        private static ContentManager _content;
        private static RenderTarget2D _renderTarget;
        private static Camera _camera;
        private static SpriteBatch _spriteBatch;

        public static void Init(GraphicsDeviceManager graphics, ContentManager content)
        {
            UpdateCamera = true;

            _graphics = graphics;
            _content = content;

            _camera = new Camera(_graphics);
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            SizeChanged();
        }

        public static void SizeChanged()
        {
            GameSettings.Width = _graphics.GraphicsDevice.Viewport.Width;
            GameSettings.Height = _graphics.GraphicsDevice.Viewport.Height;

            GameSettings.RenderTargetHeight = 480;
            GameSettings.RenderTargetWidth = (int)(((float)(GameSettings.RenderTargetHeight * GameSettings.Width)) / GameSettings.Height);

            // TODO: Optimize
            _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, GameSettings.RenderTargetWidth, GameSettings.RenderTargetHeight);

            if (_level != null)
            {
                _level.SizeChanged();
                foreach (var obj in _level.Objects)
                    obj.SizeChanged();
            }
        }

        public static void LoadLevel(ILevel level)
        {
            level.Init();

            level.Load(_content, _graphics);
            level.SizeChanged();

            foreach(var obj in level.Objects)
            {
                obj.Load(_content, _graphics);
                obj.SizeChanged();
            }

            _level = level;
        }

        public static void Update(GameTime gameTime)
        {
            InputManager.Update();

            if (UpdateCamera)
                _camera.Update(gameTime);
            _level.Update(gameTime);
            foreach (var obj in _level.Objects)
                obj.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            // Draw UI Layer
            _graphics.GraphicsDevice.SetRenderTarget(_renderTarget);
            _graphics.GraphicsDevice.Clear(Color.Transparent);
            _spriteBatch.Begin(samplerState:SamplerState.PointClamp);
            _level.DrawUI(gameTime, _spriteBatch);
            foreach(var obj in _level.Objects)
                obj.DrawUI(gameTime, _spriteBatch);
            _spriteBatch.End();
            _graphics.GraphicsDevice.SetRenderTarget(null);

            // Render game
            Utility.Effect.World = _camera.World;
            Utility.Effect.View = _camera.View;
            Utility.Effect.Projection = _camera.Projection;
            Utility.Effect.TextureEnabled = true;

            _level.Draw(gameTime, _camera);
            foreach(var obj in _level.Objects)
                obj.Draw(gameTime, _camera);

            // Render UI
            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, GameSettings.Width, GameSettings.Height), Color.White);
            _spriteBatch.End();
        }
    }
}
