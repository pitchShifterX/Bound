using GameEngine.Event;
using GameEngine.Scene;

namespace GameEngine.UI
{
    public sealed class UIContext
    {
        public ISceneContext Scene { get; }
        public UIRenderContext Render { get; }
        public IUITheme Theme { get; }

        public UIEventBus Events => Scene.UIEvents;

        public UIContext(
            ISceneContext scene,
            UIRenderContext render,
            IUITheme theme
        )
        {
            Scene = scene;
            Render = render;
            Theme = theme;
        }
    }
}