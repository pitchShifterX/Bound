using GameEngine.UI.Elements;
using GameEngine.World.Time;

namespace Mods.Bound.UI.Elements
{
    public class WorldTimer : UIText
    {
        public WorldTimer() : base("00:00")
        {
        }

        protected override void OnContextAssigned()
        {
            base.OnContextAssigned();

            Subscribe<WorldSecondEvent>(OnWorldSecond);
        }

        private void OnWorldSecond(WorldSecondEvent e)
        {
            var minutes = e.Seconds / 60;
            var seconds = e.Seconds % 60;

            SetLabel($"{minutes:00}:{seconds:00}");
        }
    }
}