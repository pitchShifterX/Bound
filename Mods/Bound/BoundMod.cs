using GameEngine.Mod;

namespace Mods.Bound
{
    public class BoundMod : Mod<BoundModConfiguration>
    {
        public BoundMod() : base(new())
        {
            try
            {
                Initialize();

                Run();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Fatal error: {e}");
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            // setup scenes
        }

        public override void Update()
        {
            
        }

        public override void Render()
        {
            
        }
    }
}