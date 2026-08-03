using GameEngine.World.Map.Parser.Lua;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers;

namespace GameEngine.World.Map
{
    /// <summary>
    /// Loads and maintains map data.
    /// </summary>
    public class MapContext : IMapContext
    {
        private string _mapsDirectory { get; init; }
        private LuaTriggerBinder? _triggers;
        private LuaMapLoader? _loader;
        private MapData? _mapData;
        private ITileCoordinateConverter? _tileGrid;

        public MapData? Data => _mapData;
        public Tile[][]? Tiles => _mapData?.Tiles;
        public ITileCoordinateConverter? TileCoordinateConverter => _tileGrid;

        public MapContext(string mapsDirectory, TriggerRegistry triggers)
        {
            _mapsDirectory = mapsDirectory;
            _triggers = new LuaTriggerBinder(triggers);

            _loader = new LuaMapLoader(_triggers);
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