using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Footstep
{
    static class GameSettings
    {
        /// <summary>
        /// Width of the game window.
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Height of the game window.
        /// </summary>
        public static int Height { get; set; }

        /// <summary>
        /// Width of the main render target for the UI.
        /// </summary>
        public static int RenderTargetWidth { get; set; }

        /// <summary>
        /// Height of the main render target for the UI.
        /// </summary>
        public static int RenderTargetHeight { get; set; }
    }
}
