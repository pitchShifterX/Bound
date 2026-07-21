using GameEngine.World.Map.Tiles;

namespace GameEngine.World.Map
{
    /// <summary>
    /// Loads and maintains map data.
    /// </summary>
    public class MapContext : IMapContext
    {
        private string _mapsDirectory { get; init; }
        private MapLuaLoader? _loader;
        private MapData? _mapData;
        private ITileCoordinateConverter? _tileGrid;

        public MapData? Data => _mapData;
        public ITileCoordinateConverter? TileCoordinateConverter => _tileGrid;

        public MapContext(string mapsDirectory)
        {
            _mapsDirectory = mapsDirectory;

            _loader = new MapLuaLoader();
        }

        public void LoadMap(string path)
        {
            if(_loader == null)
                throw new NullReferenceException("Map loader is null.");

            var fullPath = Path.Combine(_mapsDirectory, path);
            _mapData = _loader.Load(fullPath);

            if(_mapData == null || _mapData.Metadata == null)
                throw new NullReferenceException("Map data returned null.");
            
            if(_mapData.Metadata.Width == null || _mapData.Metadata.Height == null)
                throw new NullReferenceException("Map tile width or height null.");
            
            _tileGrid = new TileGrid(
                _mapData.Metadata.Width.Value, 
                _mapData.Metadata.Height.Value
            );
        }
    }
}