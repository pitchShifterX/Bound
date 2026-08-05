namespace GameEngine
{
    public static class Constants
    {
        /// <summary>
        /// The pixel size of a tile's width and height.
        /// By default 32 = 32px width, 32px height.
        /// </summary>
        public const int TileSize = 32;

        /// <summary>
        /// Cell size references the spatial hash grid whereby 
        /// maps are divided into cells for efficient processing 
        /// of entities near other entities.
        /// </summary>
        public const int CellSize = 128;
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}