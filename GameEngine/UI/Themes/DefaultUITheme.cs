using GameEngine.Graphics.Primitives;

namespace GameEngine.UI.Themes
{
    public class DefaultUITheme : IUITheme
    {
        public string FontResource => "default";

        public ContainerTheme FlexBoxes => new()
        {
            
        };

        public ContainerTheme Panels => new()
        {
            BackgroundColor = Color.White,
            BorderColor = Color.Black
        };

        public UIButtonTheme Buttons => new()
        {
            LabelColor = Color.Black,
            BorderColor = Color.Black
        };
    }
}