using GameEngine.Graphics.Rendering;
using GameEngine.Resources;
using GameEngine.Utilities.Extensions;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.Rendering.Tiles
{
    public class TileRenderer : ITileRenderer
    {
        private IMapView _map;
        private IRenderContext _renderer;
        private ICameraView _camera;
        private TilesetRegistry _registry;

        private Dictionary<string, IntPtr> _textureCache = [];

        public TileRenderer(
            IMapView map,
            IRenderContext renderer,
            ICameraView camera,
            TilesetRegistry registry
        )
        {
            _map = map;
            _renderer = renderer;
            _camera = camera;
            _registry = registry;
        }

        public void Render()
        {
            var tiles = _map.Tiles;
            var grid = _map.TileCoordinateConverter;

            if(tiles == null)
                throw new NullReferenceException("Tile list returned null, cannot render tiles.");

            if(grid == null)
                throw new NullReferenceException("Tile grid returned null, cannot render tiles.");

            if(_camera == null)
                throw new NullReferenceException("Camera returned null, cannot render tiles.");

            var visibleWorld = _camera.VisibleWorldBounds;
            var bounds = grid.GetVisibleTileBounds(visibleWorld);

            for (int x = bounds.StartX; x <= bounds.EndX; x++)
            {
                for (int y = bounds.StartY; y <= bounds.EndY; y++)
                {
                    var tile = tiles?[x][y];
                    if (tile == null) continue;

                    drawTile(tile, x, y);
                }
            }
        }

        private void drawTile(Tile tile, int x, int y)
        {
            if(tile.TilesetId == null) return;

            var tileset = _registry.GetTilesetById(tile.TilesetId);

            var worldX = x * Constants.TileSize;
            var worldY = y * Constants.TileSize;

            var screenPos = _camera.WorldPositionToScreenPosition(worldX, worldY);
            var tileZoom = (int)(Constants.TileSize * _camera.Zoom);

            if(!_textureCache.TryGetValue(tileset.Id, out var handle))
            {
                handle = _renderer.GetById<Texture>(tileset.Id)!.Handle;

                _textureCache[tileset.Id] = handle;
            }

            var source = tileset.GetSourceRectangle(tile.TileIndex).ToSDLRect();

            _renderer.DrawTexture(
                handle,
                source,
                new SDL2.SDL.SDL_Rect
                {
                    x = screenPos.x,
                    y = screenPos.y,
                    w = tileZoom,
                    h = tileZoom
                }
            );
        }
    }
}