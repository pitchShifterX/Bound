using GameEngine.Graphics.Primitives;

namespace GameEngine.UI
{
    public struct ContainerTheme
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
        public ContainerTheme FlexBoxes { get; }
        public ContainerTheme Panels { get; }
        public UIButtonTheme Buttons { get; }
    }
}