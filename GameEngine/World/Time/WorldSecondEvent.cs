using GameEngine.UI.Event;

namespace GameEngine.World.Time
{
    public class WorldSecondEvent : UIEvent
    {
        public int Seconds { get; }

        public WorldSecondEvent(int seconds)
        {
            Seconds = seconds;
        }
    }
}