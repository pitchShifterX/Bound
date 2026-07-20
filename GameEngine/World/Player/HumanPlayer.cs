namespace GameEngine.World.Player
{
    public class HumanPlayer : IPlayer
    {
        public required string Id { get; set; }
        public required string Color { get; set; }
        public bool IsHuman => true;
    }
}