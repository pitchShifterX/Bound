using GameEngine.Utilities;

namespace GameEngine.Mod
{
    public abstract class ModConfiguration
    {
        public virtual string ModName { get; init; } = "Unknown Mod";
        public Vector2<int> WindowResolution = new(1920, 1080);
        public string SettingPath { get; init; } = "config/settings.json";
    }
}