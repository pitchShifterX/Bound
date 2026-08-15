using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using Mods.Bound.Scenes.Gameplay;
using Mods.Bound.Scenes.MapEditor;

namespace Mods.Bound.Scenes.MainMenu
{
    public partial class MainMenuScene(IModContext modContext)
        : Scene(modContext)
    {
        private Texture? _menu;

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

            if(input.WasKeyPressed(KeyCode.W))
            {
                Context.ReplaceScene(() => new MapEditorScene(ModContext));
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