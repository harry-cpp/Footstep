using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class GameUI : IGameObject
    {
        private Texture2D _background;
        private Vector2 _position;
        private Rectangle _borderRectangle;
        private Color _borderColor;
        private TextureFont _font;
        private Texture2D _pixel;
        private Rectangle _avatarRectangle;
        private string _selectedItem = "Adrenaline Shot";

        public GameUI()
        {
            _borderColor = new Color(0.3f, 0.3f, 0.3f);
        }

        public override void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _background = content.Load<Texture2D>(GameContent.Texture.GameUIBackground);
            _font = new TextureFont(content.Load<Texture2D>(GameContent.Texture.Font), 9, 16);
            _pixel = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _pixel.SetData<Color>(new[] { new Color(1f, 1f, 1f, 0f) });
        }

        public override void SizeChanged()
        {
            _position = new Vector2(0, GameSettings.RenderTargetHeight - 96);
            _borderRectangle = new Rectangle(0, GameSettings.RenderTargetHeight - 96, GameSettings.RenderTargetWidth, 4);

            var avatarsize = 80;
            _avatarRectangle = new Rectangle(((92 - avatarsize) / 2), GameSettings.RenderTargetHeight - 92 + ((92 - avatarsize) / 2), avatarsize, avatarsize);
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw background
            for (int x = 0; x < GameSettings.RenderTargetWidth; x += 96)
            {
                _position.X = x;
                spriteBatch.Draw(_background, _position, Color.White);
            }

            // Draw border
            spriteBatch.Draw(_background, _borderRectangle, _borderColor);

            // Draw avatar
            spriteBatch.Draw(_pixel, _avatarRectangle, Color.White * 0.3f);

            // Draw selected item
            spriteBatch.Draw(_pixel, new Rectangle(GameSettings.RenderTargetWidth - 20 - 200, 480 - 92 + 20 - 3, 200, 20), Color.White * 0.3f);
            spriteBatch.DrawString(_font, _selectedItem, new Vector2(GameSettings.RenderTargetWidth - 20 - 200 + 9, 480 - 92 + 20), Color.Black);
        }
    }
}
