using GameEngine.Mod;
using GameEngine.Resources;
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

            var menuImagePath = Context.Paths?.GetAssetPath("images/menu.png");
            var fontPath = Context.Paths?.GetAssetPath("fonts/Inter24Regular.ttf");

            Context.ResourceManager?.Load<Font>("default", fontPath!);
            Context.SceneManager?.Push(() => new MainMenuScene(Context));
        }
    }
}