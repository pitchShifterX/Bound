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
                0,
                0,
                PixelWidth,
                PixelHeight
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

        public TileBounds GetVisibleTileBounds(Rectangle<float> worldBounds)
        {
            var min = WorldPositionToTile(
                worldBounds.X,
                worldBounds.Y
            );

            var max = WorldPositionToTile(
                worldBounds.X + worldBounds.Width,
                worldBounds.Y + worldBounds.Height
            );

            return new TileBounds
            {
                StartX = Math.Clamp(min.x, 0, _tileWidth - 1),
                EndX = Math.Clamp(max.x, 0, _tileWidth - 1),

                StartY = Math.Clamp(min.y, 0, _tileHeight - 1),
                EndY = Math.Clamp(max.y, 0, _tileHeight - 1)
            };
        }
    }
}