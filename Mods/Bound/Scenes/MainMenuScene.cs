using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;
using SDL2;

namespace Mods.Bound.Scenes
{
    public class MainMenuScene(IModContext modContext)
        : Scene(modContext)
    {
        private Texture? _opm;

        public override void Load()
        {
            Context.Load<Texture>("opm", "opm.png");
            _opm = Context.GetById<Texture>("opm");
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

                Context.SceneManager.PushScene(() => new SettingsScene(ModContext));
            }
        }

        public override void Render()
        {
            if(_opm != null)
            {
                var dst = new SDL.SDL_Rect { x = 200, y = 200, w = 337, h = 361 };
                Context.DrawTexture(_opm, null, dst);
            }
        }

        public override void Update(float? delta)
        {
        }
    }
}