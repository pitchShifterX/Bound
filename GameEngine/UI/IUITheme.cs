using GameEngine.Graphics.Primitives;

namespace GameEngine.UI
{
    public struct UIPanelTheme
    {
        public Color BackgroundColor;
        public Color BorderColor;
    }

    public struct UIButtonTheme
    {
        public Color LabelColor;
        public Color BackgroundColor;
        public Color BorderColor;
    }

    public interface IUITheme
    {
        public string FontResource { get; }
        public UIPanelTheme Panels { get; }
        public UIButtonTheme Buttons { get; }
    }
}