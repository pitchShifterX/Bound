using GameEngine.Render;
using GameEngine.Utilities;

namespace GameEngine.Settings
{
    public class Settings : IRenderSettings
    {
        public int WindowWidth { get; set; } = 1920;
        public int WindowHeight { get; set; } = 1080;
        public bool WindowFullScreen { get; set; } = false;
        public bool VerticalSync { get; set; } = false;

        /// <summary>
        /// Capped framerate; if 0, unlimited.
        /// </summary>
        public int MaxFramerate { get; set; } = 0; 

        public Vector2<int> WindowSize => new Vector2<int>(
            WindowWidth, WindowHeight
        );
    }
}