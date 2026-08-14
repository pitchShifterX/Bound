using GameEngine.Graphics.Primitives;
using GameEngine.UI;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;
using Mods.Bound.UI.Elements;
using Mods.Bound.UI.Themes;

namespace Mods.Bound.Scenes.Gameplay
{
    public partial class GameplayScene
    {
        public override IUITheme UITheme => new MenuTheme();

        public override void BuildUI()
        {
            var scoreText = new ScoreText()
                .SetColor(Color.White);

            var welcome = new UIText("Welcome!")
                .SetColor(Color.White);

            var layout = new UIFlexBox(new Fill(), new Fixed(75))
                .SetBackgroundColor(Color.Transparent)
                .SetJustifyContent(FlexJustify.SpaceBetween)
                .SetBorderColor(Color.Green)
                .SetPadding(UISpacing.All(10));

            layout.AddChild(scoreText);
            layout.AddChild(welcome);

            UI.Root.AddChild(layout);
        }
    }
}