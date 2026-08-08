using GameEngine.Graphics.Primitives;

namespace GameEngine.UI
{
    public struct UIButtonTheme
    {
        public Color LabelColor;
        public Color BorderColor;
    }

    public interface IUITheme
    {
        public string FontResource { get; }
        public UIButtonTheme Buttons { get; }
    }
}