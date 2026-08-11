using GameEngine.Graphics.Primitives;
using GameEngine.UI;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;
using Mods.Bound.Scenes.Gameplay;
using Mods.Bound.UIThemes;

namespace Mods.Bound.Scenes.MainMenu
{
    public partial class MainMenuScene
    {
        public override IUITheme UITheme => new MenuTheme();

        public override void BuildUI()
        {
            if(_menu != null)
            {
                var menuImage = new UIImage(_menu);
                
                UI.Root.AddChild(menuImage);
            }

            var play = playTestMapButton();
            var quit = quitButton();

            UI.Root.AddChild(play);
            UI.Root.AddChild(quit);
        }

        private UIButton playTestMapButton()
        {
            return new UIButton(new Fixed(300), new Fixed(50))
                .SetLabel("Select Map")
                .SetAction(() => Context.ReplaceScene(() => new MapLobbyScene(ModContext)))
                .SetMargin(new UISpacing(300, 0, 0, 0))
                .SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .SetBackgroundColor(Color.Transparent)
                .SetBorderColor(Color.Green);
        }

        private UIButton quitButton()
        {
            return new UIButton(new Fixed(300), new Fixed(50))
                .SetLabel("Quit")
                .SetAction(() => Context.QuitMod())
                .SetMargin(new UISpacing(450, 0, 0, 0))
                .SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center)
                .SetBackgroundColor(Color.Transparent)
                .SetBorderColor(Color.Red);
        }
    }
}