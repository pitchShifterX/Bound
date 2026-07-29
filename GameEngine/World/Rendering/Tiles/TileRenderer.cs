using GameEngine.Graphics.Rendering;
using GameEngine.Resources;
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

        private Dictionary<string, IntPtr> _textureCache = [];

        public TileRenderer(
            IMapView map,
            IRenderContext renderer,
            ICameraView camera
        )
        {
            _map = map;
            _renderer = renderer;
            _camera = camera;
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
            if(tile.TextureId == null) return;

            var worldX = x * Constants.TileSize;
            var worldY = y * Constants.TileSize;

            var screenPos = _camera.WorldPositionToScreenPosition(worldX, worldY);
            var tileZoom = (int)(Constants.TileSize * _camera.Zoom);

            if(!_textureCache.TryGetValue(tile.TextureId, out var handle))
            {
                handle = _renderer.GetById<Texture>(tile.TextureId)!.Handle;

                _textureCache[tile.TextureId] = handle;
            }

            _renderer.DrawTexture(
                handle,
                null,
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