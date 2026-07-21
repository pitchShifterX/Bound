using GameEngine.World.Map.Tiles;

namespace GameEngine.World.Map
{
    public interface IMapContext
    {
        public MapData? Data { get; }
        public ITileCoordinateConverter? TileCoordinateConverter { get; }
        public void LoadMap(string path);
    }
}