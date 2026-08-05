using GameEngine.Utilities;

namespace GameEngine.World.Map.Tiles
{
    public class TilesetDefinition
    {
        public required string Id { get; init; }
        public required string TexturePath { get; init; }

        public int Columns { get; init; }
        public int Rows { get; init; }

        public TileDefinition[] TileDefinitions { get; init; } = [];

        public Rectangle<int> GetSourceRectangle(int tileIndex)
        {
            var maxTiles = Columns * Rows;

            if(tileIndex < 0 || tileIndex >= maxTiles)
                throw new ArgumentOutOfRangeException(nameof(tileIndex));

            var column = tileIndex % Columns;
            var row = tileIndex / Columns;

            var xPosition = column * Constants.TileSize;
            var yPosition = row * Constants.TileSize;

            return new Rectangle<int>(
                xPosition,
                yPosition,
                Constants.TileSize,
                Constants.TileSize
            );
        }
    }
}