using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Footstep
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        VertexPositionNormalTexture[] floorVerts;
        BasicEffect effect;
        Texture2D checkerboardTexture;
        Camera camera;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1700;
            graphics.PreferredBackBufferHeight = 900;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Window.Title = "Monogame 3D Tests";
            IsMouseVisible = true;

            floorVerts = new VertexPositionNormalTexture[6];

            floorVerts[0].Position = new Vector3(-20, -20, 0);
            floorVerts[1].Position = new Vector3(-20, 20, 0);
            floorVerts[2].Position = new Vector3(20, -20, 0);

            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[4].Position = new Vector3(20, 20, 0);
            floorVerts[5].Position = floorVerts[2].Position;

            int repetition = 20;

            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, repetition);
            floorVerts[2].TextureCoordinate = new Vector2(repetition, 0);

            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(repetition, repetition);
            floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;

            effect = new BasicEffect(graphics.GraphicsDevice);

            camera = new Camera(graphics.GraphicsDevice);

            base.Initialize();
        }
        void Window_ClientSizeChanged(object sender, EventArgs e) {
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            checkerboardTexture = Content.Load<Texture2D>("Checkerboard");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState newKeyboard = Keyboard.GetState();

            if (newKeyboard.IsKeyDown(Keys.Q)) {
                camera.moveOut();
            }
            if (newKeyboard.IsKeyDown(Keys.E)) {
                camera.moveIn();
            }
            if (newKeyboard.IsKeyDown(Keys.W)) {
                camera.moveUp();
            }
            if (newKeyboard.IsKeyDown(Keys.S)) {
                camera.moveDown();
            }
            if (newKeyboard.IsKeyDown(Keys.A)) {
                camera.moveLeft();
            }
            if (newKeyboard.IsKeyDown(Keys.D)) {
                camera.moveRight();
            }
            
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawGround();

            base.Draw(gameTime);
        }

        void DrawGround() {
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;

            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;

            foreach(var pass in effect.CurrentTechnique.Passes) {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, floorVerts, 0, 2);
            }
        }
    }
}
