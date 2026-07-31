namespace GameEngine.World.Player
{
    public class PlayerData : IPlayer
    {
        public string? Name { get; set; }
        public required int Id { get; set; }
        public required string Color { get; set; }
        public bool IsHuman { get; set; }
    }
}