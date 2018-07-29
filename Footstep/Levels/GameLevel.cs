using Microsoft.Xna.Framework.Input;

namespace Footstep
{
    class GameLevel : ILevel
    {
        public override void Init()
        {
            Objects.Add(new Floor());
            Objects.Add(new Wall());
            Objects.Add(new Ceiling());
            Objects.Add(new GameUI());

            Mouse.SetPosition(GameSettings.Width / 2, GameSettings.Height / 2);
        }
    }
}
