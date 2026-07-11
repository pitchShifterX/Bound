using GameEngine.Mod;

namespace Mods.Bound
{
    public class BoundMod : Mod<BoundModConfiguration>
    {
        public BoundMod() : base(new())
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            // setup scenes
        }

        public override void Start()
        {
            try
            {
                Run();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Fatal error: {e}");
            }
            finally
            {
                Close();
            }
        }

        public override void Update()
        {
            
        }

        public override void Render()
        {
            
        }
    }
}