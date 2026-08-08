using GameEngine.Graphics.Primitives;
using GameEngine.UI;

namespace Mods.Bound.UIThemes
{
    public class MenuTheme : IUITheme
    {
        public string FontResource => "default";

        public UIButtonTheme Buttons => new UIButtonTheme
        {
            LabelColor = Color.White,
            BorderColor = Color.Green
        };
    }
}