using GameEngine.World.Map.Tiles;

namespace GameEngine.World.Map
{
    public interface IMapView
    {
        public Tile[][]? Tiles { get; }
        public ITileCoordinateConverter? TileCoordinateConverter { get; }
        public MapData? Data { get; }
    }
}