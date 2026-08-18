namespace GameEngine.UI.Event.Types
{
    public class TileHoverEvent : UIEvent
    {
        public int X { get; }
        public int Y { get; }

        public TileHoverEvent(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}