using GameEngine.Graphics.Primitives;
using GameEngine.UI.Elements;
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
            var rootCenter = UI.Root.Center;

            var testMapBtnRect = new Rectangle<float>
            {
                X = rootCenter.x - 150,
                Y = rootCenter.y * 1.3f,
                Width = 300,
                Height = 50
            };

            return new UIButton(testMapBtnRect, () => Context.ReplaceScene(() => new GameplayScene(ModContext)))
            {
                Label = "Play Test Map",
                BorderColor = Color.Green
            };
        }

        private UIButton quitButton()
        {
            var rootCenter = UI.Root.Center;

            var quitBtnRect = new Rectangle<float>
            {
                X = rootCenter.x - 150,
                Y = rootCenter.y * 1.5f,
                Width = 300,
                Height = 50
            };

            return new UIButton(quitBtnRect, () => Context.QuitMod())
            {
                Label = "Quit",
                BorderColor = Color.Red
            };
        }
    }
}