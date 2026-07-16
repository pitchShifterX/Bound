using GameEngine.Audio;
using GameEngine.Event;
using GameEngine.Event.Input;
using GameEngine.Render;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Settings;
using GameEngine.Utilities;
using GameEngine.Window;

namespace GameEngine.Mod
{
    public class ModContext : IModContext
    {
        public IModPath? Paths { get; set; }
        public ISettingsController? SettingsManager { get; set; }
        public IWindowController? WindowManager { get; set; }
        public IRendererController? RendererManager { get; set; }
        public IEventController? EventManager { get; set; }
        public IInputController? InputManager { get; set; }
        public IResourceController? ResourceManager { get; set; }
        public IAudioController? AudioManager { get; set; }
        public ISceneController? SceneManager { get; set; }
    }
}