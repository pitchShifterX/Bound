using GameEngine.Event.Input;
using GameEngine.Graphics;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Utilities;
using SDL2;

namespace Mods.Bound.Scenes
{
    public class MainMenuScene(IModContext modContext)
        : Scene(modContext)
    {
        private Audio? _menuMusic;
        private Font? _font;
        private Texture? _menu;
        private Vector2<int> _windowResolution => ModContext.SettingsManager!.Settings.WindowSize;

        private int counter = 0;
        private string text = "counter:";

        private string getText() => $"{text} {counter}";

        public override void Load()
        {
            var menuImagePath = Context.Paths.GetAssetPath("images/menu.png");

            Context.Load<Texture>("menu", menuImagePath);
            _menu = Context.GetById<Texture>("menu");

            _font = Context.GetById<Font>("default");
        }

        public override void ProcessInput(IRecordInput input)
        {
            if(input.WasKeyPressed(KeyCode.Enter))
            {
                Console.WriteLine("pressed enter");
            }

            if(input.WasKeyPressed(KeyCode.A))
            {
                Console.WriteLine("changing scene");

                // Context.StopMusic();
                Context.PushScene(() => new SettingsScene(ModContext));
            }

            if(input.WasKeyPressed(KeyCode.G))
            {
                counter++;
            }

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
            if(_menu != null)
            {
                var dst = new SDL.SDL_Rect { x = 0, y = 0, w = _windowResolution.x, h = _windowResolution.y };
                Context.DrawTexture(_menu, null, dst);
            }

            if(_font != null)
            {
                var dst = new SDL.SDL_Rect { x = 500, y = 500 };
                Context.DrawText(_font, getText(), Color.White, dst);
            }
        }

        public override void Update(float? delta)
        {
        }
    }
}