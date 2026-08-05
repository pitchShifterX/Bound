using GameEngine.Utilities;
using GameEngine.World.Map.Tiles;

namespace GameEngine.World.Map
{
    public interface IMapContext : IMapView
    {
        public void LoadMap(string path);
        public IEnumerable<Tile> GetTiles(Rectangle<float> bounds);
    }
}