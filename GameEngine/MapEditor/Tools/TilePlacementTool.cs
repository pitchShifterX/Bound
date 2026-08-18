using GameEngine.MapEditor.Input;
using GameEngine.Utilities;
using GameEngine.World.Map.Tiles;

namespace GameEngine.MapEditor.Tools
{
    public class TilePlacementTool : PlacementTool
    {
        public string TilesetId { get; init; }
        public int TileIndex { get; init; }

        public TilePlacementTool(string tileset, int tile)
        {
            TilesetId = tileset;
            TileIndex = tile;
        }

        public override void Place(EditorContext context, Vector2<int> position)
        {
            if (context.Map?.Data?.Tiles == null)
                return;

            var tile = new Tile
            {
                TilesetId = TilesetId,
                TileIndex = TileIndex
            };

            context.Map.Data.Tiles[position.x][position.y] = tile;
        }

        public override void Process(EditorContext context, EditorInput input)
        {
            
            if(!input.LeftPressed)
                return;

            Place(context, input.MouseTilePosition);
        }
    }
}