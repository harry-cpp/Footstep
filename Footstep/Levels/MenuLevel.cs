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
        private TextureFont _font;

        public override void Init()
        {
            Utility.Game.IsMouseVisible = true;
            LevelManager.UpdateCamera = false;

            Objects.Add(new Floor());
            Objects.Add(new Wall());
            Objects.Add(new Ceiling());

            _menuItems = new List<string>();
            _menuItems.Add("Play");
            _menuItems.Add("Quit");
        }

        public override void Load(ContentManager content, GraphicsDeviceManager graphics)
        {
            _font = new TextureFont(content.Load<Texture2D>(GameContent.Texture.Font), 9, 16);
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
                else
                    Utility.Game.Exit();
            }
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var pos = new Vector2(GameSettings.RenderTargetWidth - 100, GameSettings.RenderTargetHeight / 2);

            for (int i = 0; i < _menuItems.Count; i++)
            {
                spriteBatch.DrawString(_font, _menuItems[i], pos, i == _selectedIndex ? Color.White : Color.Gray);
                pos.Y += 16 + 6;
            }
        }
    }
}
