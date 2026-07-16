using GameEngine.Utilities;

namespace GameEngine.Settings
{
    public class SettingsManager : ISettingsController
    {
        private readonly Json<Settings> _serializer;

        public Settings Settings => _serializer.Value!;

        public SettingsManager(string settingsPath)
        {
            _serializer = new Json<Settings>(settingsPath);

            if (!_serializer.TryLoad())
            {
                Log.Info("Settings file not found; creating defaults.");

                _serializer.TryWrite();
            }
        }

        public void UpdateWindowResolution(int width, int height)
        {
            Settings.WindowWidth = width;
            Settings.WindowHeight = height;
        }

        public void UpdateWindowResolution(Vector2<int> resolution)
        {
            Settings.WindowWidth = resolution.x;
            Settings.WindowHeight = resolution.y;
        }

        public void UpdateWindowFullscreen(bool value)
        {
            Settings.WindowFullScreen = value;
        }

        public void UpdateVerticalSync(bool value)
        {
            Settings.VerticalSync = value;
        }

        public void UpdateMaxFramerate(int value)
        {
            Settings.MaxFramerate = value;
        }

        public void Save()
        {
            _serializer.TryWrite();
        }
    }
}