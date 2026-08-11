using GameEngine.Graphics.Primitives;
using GameEngine.UI;

namespace Mods.Bound.UIThemes
{
    public class MenuTheme : IUITheme
    {
        public string FontResource => "default";

        public UIPanelTheme Panels => new UIPanelTheme
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