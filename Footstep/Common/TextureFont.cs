using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    public class TextureFont
    {
        private Texture2D _texture;
        private int _fontWidth, _fontHeight;

        public TextureFont(Texture2D texture, int fontWidth, int fontHeight)
        {
            _texture = texture;
            _fontWidth = fontWidth;
            _fontHeight = fontHeight;
        }

        public Vector2 MeasureString(string s)
        {
            return new Vector2(s.Length * _fontWidth, _fontHeight);
        }

        public void DrawChar(SpriteBatch spriteBatch, char c, Vector2 position, Color color)
        {
            int ypos = ((c - 33) / 32) * _fontHeight;
            int xpos = ((c - 33) % 32) * _fontWidth;

            spriteBatch.Draw(
                texture: _texture,
                sourceRectangle: new Rectangle(xpos, ypos, _fontWidth, _fontHeight),
                position: position,
                color: color
            );
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            foreach (var c in text)
            {
                position.X += _fontWidth;
                DrawChar(spriteBatch, c, position, color);
            }
        }
    }

    static class TextureFontExtension
    {
        public static void DrawString(this SpriteBatch spriteBatch, TextureFont font, string text, Vector2 position, Color color)
        {
            font.DrawString(spriteBatch, text, position, color);
        }
    }
}
