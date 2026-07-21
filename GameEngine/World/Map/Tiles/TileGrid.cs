using GameEngine.Utilities;

namespace GameEngine.World.Map.Tiles
{
    public class TileGrid : ITileCoordinateConverter
    {
        private int _tileWidth;
        private int _tileHeight;

        public int TileWidth => _tileWidth;
        public int TileHeight => _tileHeight;

        public int PixelWidth => _tileWidth * Constants.TileSize;
        public int PixelHeight => _tileHeight * Constants.TileSize;

        public Rectangle<float> WorldBounds =>
            new(
                X: 0,
                Y: 0,
                Width: PixelWidth,
                Height: PixelHeight
            );

        public TileGrid(int tileWidth, int tileHeight)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public Vector2<int> WorldPositionToTile(float positionX, float positionY)
        {
            int tileX = (int)(positionX / Constants.TileSize);
            int tileY = (int)(positionY / Constants.TileSize);

            tileX = Math.Clamp(tileX, 0, _tileWidth - 1);
            tileY = Math.Clamp(tileY, 0, _tileHeight - 1);

            return new(x: tileX, y: tileY);
        }

        public Vector2<float> TileToWorldPosition(int tileX, int tileY)
        {
            return new(x: tileX * Constants.TileSize, y: tileY * Constants.TileSize);
        }
    }
}