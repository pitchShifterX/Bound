using GameEngine.UI;

namespace Mods.Bound.UI.Events
{
    public class ScoreEvent : UIEvent
    {
        public int PlayerId { get; }
        public int NewScore { get; }

        public ScoreEvent(int player, int score)
        {
            PlayerId = player;
            NewScore = score;
        }
    }
}