namespace GameEngine.World.Map.Tiles
{
    /// <summary>
    /// Currently not implemented.
    /// </summary>
    public class TileDefinition
    {
        public bool IsWalkable { get; init; } = true;
        public float MovementSpeedMultiplier { get; init; } = 1.0f;
    }
}