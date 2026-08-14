namespace GameEngine.World.Player
{
    /// <summary>
    /// Currently associated with maps until multiplayer functionality is supported.
    /// </summary>
    public interface IPlayer
    {
        public string? Name { get; }
        public int Id { get; }
        public string Color { get; }
        public bool IsHuman { get; }
        public Dictionary<string, object> CustomVariables { get; }
    }
}