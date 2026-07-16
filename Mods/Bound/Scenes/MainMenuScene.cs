using GameEngine.Event.Input;
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
        private Texture? _menu;
        private Vector2<int> _windowResolution => ModContext.SettingsManager!.Settings.WindowSize;
        private IModPath _paths => ModContext.Paths!;

        public override void Load()
        {
            var menuImagePath = _paths.GetAssetPath("images/menu.png");

            Context.Load<Texture>("menu", menuImagePath);
            _menu = Context.GetById<Texture>("menu");

            // Context.Load<Audio>("menuMusic", menuMusicPath);
            // Context.PlayMusic("menuMusic");
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

                Context.StopMusic();
                Context.SceneManager.PushScene(() => new SettingsScene(ModContext));
            }
        }

        public override void Render()
        {
            if(_menu != null)
            {
                var dst = new SDL.SDL_Rect { x = 0, y = 0, w = _windowResolution.x, h = _windowResolution.y };
                Context.DrawTexture(_menu, null, dst);
            }
        }

        public override void Update(float? delta)
        {
        }
    }
}