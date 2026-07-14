using GameEngine.Mod;
using Mods.Bound.Scenes;

namespace Mods.Bound
{
    public class BoundMod : Mod<BoundModConfiguration>
    {
        public BoundMod() : base(new())
        {
            Console.WriteLine("Bound initializing...");
        }

        public override void Initialize()
        {
            base.Initialize();

            Context.SceneManager?.PushScene(() => new MainMenuScene(Context));
        }
    }
}