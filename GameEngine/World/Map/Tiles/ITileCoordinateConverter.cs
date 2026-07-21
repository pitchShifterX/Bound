using GameEngine.Utilities;

namespace GameEngine.World.Map.Tiles
{
    public interface ITileCoordinateConverter
    {
        /// <summary>
        /// Total width of map in pixels.
        /// </summary>
        public int PixelWidth { get; }

        /// <summary>
        /// Total height of map in pixels.
        /// </summary>
        public int PixelHeight { get; }

        /// <summary>
        /// Returns the world boundaries.
        /// </summary>
        public Rectangle<float> WorldBounds { get; }

        /// <summary>
        /// Get the tile coordinates from a pixel in the world.
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <returns></returns>
        public Vector2<int> WorldPositionToTile(float positionX, float positionY);

        /// <summary>
        /// Get the pixel location from a tile in the world.
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <returns></returns>
        public Vector2<float> TileToWorldPosition(int tileX, int tileY);
    }
}