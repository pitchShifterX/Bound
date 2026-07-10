using GameEngine.Utilities;

namespace GameEngine.Settings
{
    public interface ISettingsController
    {
        public Settings Settings { get; }

        public void UpdateWindowResolution(int width, int height);
        public void UpdateWindowResolution(Vector2<int> resolution);

        public void UpdateWindowFullscreen(bool value);
        public void UpdateVerticalSync(bool value);
        public void UpdateMaxFramerate(int value);
        
        public void Save();
    }
}