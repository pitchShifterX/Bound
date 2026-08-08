using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.UI;
using Mods.Bound.Scenes.Gameplay;
using Mods.Bound.UIThemes;

namespace Mods.Bound.Scenes.MainMenu
{
    public partial class MainMenuScene(IModContext modContext)
        : Scene(modContext)
    {
        private Texture? _menu;

        public override IUITheme UITheme => new MenuTheme();

        public override void Load()
        {
            var menuImagePath = Context.Paths.GetAssetPath("images/menu.png");

            Context.Load<Texture>("menu", menuImagePath);
            _menu = Context.GetById<Texture>("menu");
        }

        public override void ProcessInput(IRecordInput input)
        {
            UI.Process(input);

            if(input.WasKeyPressed(KeyCode.P))
            {
                Context.ReplaceScene(() => new GameplayScene(ModContext));
            }

            if(input.WasKeyPressed(KeyCode.Q))
            {
                Console.WriteLine("quitting");

                Context.QuitMod();
            }
        }

        public override void Render()
        {
            UI.Render();
        }

        public override void Update(float? delta)
        {
            UI.Update(delta);
        }
    }
}