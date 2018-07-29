using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    abstract class ILevel : IGameObject
    {
        public List<IGameObject> Objects;

        public ILevel()
        {
            Objects = new List<IGameObject>();
        }

        public abstract void Init();
    }
}
