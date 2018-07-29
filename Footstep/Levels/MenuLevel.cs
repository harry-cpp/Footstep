using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class MenuLevel : ILevel
    {
        private int _selectedIndex;
        private List<string> _menuItems;
        private TextureFont _titleFont, _menuFont, _footerFont;
        private string _title, _footer;
        private Vector2 _titlePosition, _menuPosition, _footerPosition;

        public override void Init()
        {
            Utility.Game.IsMouseVisible = true;
            LevelManager.UpdateCamera = false;

            Objects.Add(new Floor());
            Objects.Add(new Wall());
            Objects.Add(new Ceiling());

            _menuItems = new List<string>();
            _menuItems.Add("Play");
            _menuItems.Add("Opitions");
            _menuItems.Add("About");
            _menuItems.Add("Quit");
        }

        public override void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _titleFont = new TextureFont(content.Load<Texture2D>(GameContent.Texture.Font), 9, 16, 1.5f);
            _menuFont = new TextureFont(content.Load<Texture2D>(GameContent.Texture.Font), 9, 16, 1f);
            _footerFont = new TextureFont(content.Load<Texture2D>(GameContent.Texture.Font), 9, 16, 1f);

            _title = "Footstep";
            _footer = "Version v0.1";
        }

        public override void SizeChanged()
        {
            var titleSize = _titleFont.MeasureString(_title);
            _titlePosition = new Vector2(GameSettings.RenderTargetWidth - titleSize.X - 10f, 140f);

            _menuPosition = new Vector2(0, 140f + titleSize.Y + 6f + 10f);

            var footerSize = _menuFont.MeasureString(_footer);
            _footerPosition = new Vector2(10f, GameSettings.RenderTargetHeight - footerSize.Y - 5f);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsPressed(InputKey.MenuUp))
                _selectedIndex -= 1;
            
            if (InputManager.IsPressed(InputKey.MenuDown))
                _selectedIndex += 1;

            if (_selectedIndex < 0)
                _selectedIndex = _menuItems.Count - 1;

            if (_selectedIndex >= _menuItems.Count)
                _selectedIndex = 0;

            if (InputManager.IsPressed(InputKey.MenuSelect))
            {
                if (_menuItems[_selectedIndex] == "Play")
                    LevelManager.LoadLevel(new GameLevel());
                else if (_menuItems[_selectedIndex] == "Quit")
                    Utility.Game.Exit();
            }
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw Title
            spriteBatch.DrawString(_titleFont, _title, _titlePosition, Color.White);

            // Draw Menu
            var pos = _menuPosition;
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var text = "[" + _menuItems[i] + "]";
                var size = _menuFont.MeasureString(text);

                pos.X = GameSettings.RenderTargetWidth - size.X - 10f;

                spriteBatch.DrawString(_menuFont, text, pos, i == _selectedIndex ? Color.White : Color.Gray);

                pos.Y += size.Y + 6;
            }

            // Draw version info
            spriteBatch.DrawString(_menuFont, _footer, _footerPosition, Color.White);

        }
    }
}
