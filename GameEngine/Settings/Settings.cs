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
        /// Capped framerate; 60 by default
        /// </summary>
        public int MaxFramerate { get; set; } = 60; 

        public Vector2<int> WindowSize => new Vector2<int>(
            WindowWidth, WindowHeight
        );
    }
}