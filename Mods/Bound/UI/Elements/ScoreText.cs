using GameEngine.UI.Elements;
using GameEngine.Utilities;
using Mods.Bound.UI.Events;

namespace Mods.Bound.UI.Elements
{
    public sealed class ScoreText : UIText
    {
        public ScoreText() : base("Score: 0")
        {
            
        }

        protected override void OnContextAssigned()
        {
            base.OnContextAssigned();

            Subscribe<ScoreEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreEvent e)
        {
            Log.Info("Score event notified.");
            
            SetLabel($"Score: {e.NewScore}");
        }
    }
}