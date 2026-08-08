using GameEngine.Mod;
using GameEngine.Resources;
using Mods.Bound.Scenes.MainMenu;

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

            var fontPath = Context.Paths?.GetAssetPath("fonts/Inter24Regular.ttf");

            Context.ResourceManager?.Load<Font>("default", fontPath!);
            Context.SceneManager?.Push(() => new MainMenuScene(Context));
        }
    }
}