using GameEngine.Graphics.Primitives;
using GameEngine.UI;

namespace Mods.Bound.UI.Themes
{
    public class MapEditorTheme : IUITheme
    {
        public string FontResource => "normal";

        public Color PrimaryBackground => new(41, 44, 45);
        public Color SecondaryBackground => new(36, 40, 41);

        public ContainerTheme FlexBoxes => new ContainerTheme
        {
            BackgroundColor = new(21, 22, 22)
        };

        public ContainerTheme Panels => new ContainerTheme
        {
            BorderColor = new(17, 17, 17, 50)
        };

        public UIButtonTheme Buttons => new UIButtonTheme
        {
            LabelColor = Color.White,
            BackgroundColor = new(17, 17, 17),
            BorderColor = Color.Green
        };
    }
}