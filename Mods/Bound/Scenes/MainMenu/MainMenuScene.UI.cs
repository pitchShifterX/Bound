using GameEngine.Graphics.Primitives;
using GameEngine.UI;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;
using Mods.Bound.Scenes.Gameplay;
using Mods.Bound.UI.Themes;

namespace Mods.Bound.Scenes.MainMenu
{
    public partial class MainMenuScene
    {
        public override IUITheme UITheme => new MenuTheme();

        public override void BuildUI()
        {
            if(_menu == null) return;
            
            var menuImage = new UIImage(_menu, new Fill(), new Fill());

            var buttons = new UIFlexBox(new Fill(), new Fill())
                .SetDirection(FlexDirection.Column)
                .SetAlignItems(FlexAlign.Center)
                .SetJustifyContent(FlexJustify.Center)
                .SetPadding(new UISpacing(500, 0, 0, 0))
                .SetGap(20);

            buttons.AddChild(playTestMapButton());
            buttons.AddChild(quitButton());

            menuImage.AddChild(buttons);

            UI.Root.AddChild(menuImage);
        }

        private UIButton playTestMapButton()
        {
            return new UIButton(new Fixed(300), new Fixed(50))
                .SetLabel("Select Map")
                .SetAction(() => Context.ReplaceScene(() => new MapLobbyScene(ModContext)))
                .SetMargin(new UISpacing(0, 0, 0, 0))
                .SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .SetBackgroundColor(Color.Transparent)
                .SetBorderColor(Color.Green);
        }

        private UIButton quitButton()
        {
            return new UIButton(new Fixed(300), new Fixed(50))
                .SetLabel("Quit")
                .SetAction(() => Context.QuitMod())
                .SetMargin(new UISpacing(0, 0, 0, 0))
                .SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .SetBackgroundColor(Color.Transparent)
                .SetBorderColor(Color.Red);
        }
    }
}