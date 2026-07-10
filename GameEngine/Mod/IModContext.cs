using GameEngine.Event;
using GameEngine.Render;
using GameEngine.Settings;
using GameEngine.Window;

namespace GameEngine.Mod
{
    public interface IModContext
    {
        public ISettingsController? SettingsManager { get; set; }
        public IWindowController? WindowManager { get; set; }
        public IRendererController? RendererManager { get; set; }
        public IEventController? EventManager { get; set; }
        public IReceiveEvents? InputManager { get; set; }
    }
}