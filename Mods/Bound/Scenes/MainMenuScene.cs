using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.Resources;
using GameEngine.Scene;

namespace Mods.Bound.Scenes
{
    public class MainMenuScene(IModContext modContext)
        : Scene(modContext)
    {
        public override void Load()
        {
            Context.Load<Texture>("opm", "opm.png");
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
            
        }

        public override void Update(float? delta)
        {
        }
    }
}