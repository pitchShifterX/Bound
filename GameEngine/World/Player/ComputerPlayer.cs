namespace GameEngine.World.Player
{
    public class ComputerPlayer : IPlayer
    {
        public required string Name { get; set; }
        public required int Id { get; set; }
        public required string Color { get; set; }
        public bool IsHuman => false;
        public Dictionary<string, object> CustomVariables { get; init; } = [];
    }
}