namespace GameEngine.World.Player
{
    public class HumanPlayer : IPlayer
    {
        public required string Name { get; set; }
        public required int Id { get; set; }
        public required string Color { get; set; }
        public bool IsHuman => true;
    }
}