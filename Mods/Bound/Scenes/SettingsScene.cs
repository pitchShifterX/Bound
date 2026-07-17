using GameEngine.Event.Input;
using GameEngine.Mod;
using GameEngine.Scene;

namespace Mods.Bound.Scenes
{
    public class SettingsScene(IModContext modContext)
        : Scene(modContext)
    {
        public override void Load()
        {
            
        }

        public override void ProcessInput(IRecordInput input)
        {
            if(input.WasKeyPressed(KeyCode.B))
            {
                Console.WriteLine("pressed b");
            }

            if(input.WasKeyPressed(KeyCode.C))
            {
                Console.WriteLine("leaving settings");

                Context.PopScene();
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