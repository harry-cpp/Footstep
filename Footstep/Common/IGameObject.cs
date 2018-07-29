using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    class IGameObject
    {
        public virtual void Load(ContentManager content, GraphicsDeviceManager graphics)
        {

        }

        public virtual void SizeChanged()
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, Camera camera)
        {

        }

        public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
