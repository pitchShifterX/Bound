namespace GameEngine.World.Player
{
    public interface IPlayer
    {
        public string Id { get; }
        public string Color { get; }
        public bool IsHuman { get; }
    }
}