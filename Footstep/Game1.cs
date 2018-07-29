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
            IsMouseVisible = false;

            Utility.Game = this;
            Utility.Effect = new BasicEffect(_graphics.GraphicsDevice);
            Utility.Window = Window;

            Utility.GameMap = MapGenerator.GenerateMap();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LevelManager.Init(_graphics, Content);
            LevelManager.LoadLevel(new MenuLevel());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            LevelManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            LevelManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}