namespace GameEngine.World.Player
{
    public class ComputerPlayer : IPlayer
    {
        public required string Id { get; set; }
        public required string Color { get; set; }
        public bool IsHuman => false;
    }
}