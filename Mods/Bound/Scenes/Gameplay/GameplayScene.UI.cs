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

            var wip = new UIText("Work in Progress")
                .SetColor(Color.Red);

            var timer = new WorldTimer()
                .SetColor(Color.White);

            var layout = new UIFlexBox(new Fill(), new Fixed(75))
                .SetBackgroundColor(Color.Transparent)
                .SetJustifyContent(FlexJustify.SpaceBetween)
                .SetPadding(UISpacing.All(10));

            layout.AddChild(scoreText);
            layout.AddChild(wip);
            layout.AddChild(timer);

            UI.Root.AddChild(layout);
        }
    }
}