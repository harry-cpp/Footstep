using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    public class TextureFont
    {
        private Texture2D _texture;
        private int _fontWidth, _fontHeight;
        private float _scaleFactor;

        public TextureFont(Texture2D texture, int fontWidth, int fontHeight) : this (texture, fontWidth, fontHeight, 1f)
        {

        }

        public TextureFont(Texture2D texture, int fontWidth, int fontHeight, float scaleFactor)
        {
            _texture = texture;
            _fontWidth = fontWidth;
            _fontHeight = fontHeight;
            _scaleFactor = scaleFactor;
        }

        public float CharacterWidth => _fontWidth * _scaleFactor;

        public float CharacterHeight => _fontHeight * _scaleFactor;

        public Vector2 MeasureString(string s)
        {
            return new Vector2(s.Length * _fontWidth * _scaleFactor, _fontHeight * _scaleFactor);
        }

        public void DrawChar(SpriteBatch spriteBatch, char c, Vector2 position, Color color)
        {
            int ypos = ((c - 33) / 32) * _fontHeight;
            int xpos = ((c - 33) % 32) * _fontWidth;

            spriteBatch.Draw(
                texture: _texture,
                sourceRectangle: new Rectangle(xpos, ypos, _fontWidth, _fontHeight),
                destinationRectangle: new Rectangle((int)(position.X), (int)(position.Y), (int)(_fontWidth * _scaleFactor), (int)(_fontHeight * _scaleFactor)),
                color: color
            );
        }

        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            foreach (var c in text)
            {
                position.X += _fontWidth * _scaleFactor;
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
