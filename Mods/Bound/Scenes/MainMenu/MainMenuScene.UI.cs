using GameEngine.Graphics.Primitives;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;
using GameEngine.Utilities;
using Mods.Bound.Scenes.Gameplay;

namespace Mods.Bound.Scenes.MainMenu
{
    public partial class MainMenuScene
    {
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
            return new UIButton(
                new Fixed(300),
                new Fixed(50),
                "Select Map",
                () => Context.ReplaceScene(() => new MapLobbyScene(ModContext))
            )
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new UISpacing()
                {
                    Top = 300
                },
                LabelColor = Color.White
            };
        }

        private UIButton quitButton()
        {
            return new UIButton(
                new Fixed(300),
                new Fixed(50),
                "Quit",
                () => Context.QuitMod()
            )
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new UISpacing()
                {
                    Top = 450
                },
                LabelColor = Color.White,
                BorderColor = Color.Red
            };
        }
    }
}