using GameEngine.Event;
using GameEngine.Render;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Settings;
using GameEngine.Window;

namespace GameEngine.Mod
{
    public class ModContext : IModContext
    {
        public ISettingsController? SettingsManager { get; set; }
        public IWindowController? WindowManager { get; set; }
        public IRendererController? RendererManager { get; set; }
        public IEventController? EventManager { get; set; }
        public IReceiveEvents? InputManager { get; set; }
        public IResourceController? ResourceManager { get; set; }
        public ISceneController? SceneManager { get; set; }
    }
}