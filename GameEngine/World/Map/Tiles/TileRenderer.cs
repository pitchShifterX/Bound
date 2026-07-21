using GameEngine.Render;

namespace GameEngine.World.Map.Tiles
{
    public class TileRenderer : ITileRenderer
    {
        private IDrawTexture _renderer;
        private ITileCoordinateConverter _grid;
        private ICameraView _camera;

        public TileRenderer(
            IDrawTexture renderer,
            ITileCoordinateConverter grid,
            ICameraView camera
        )
        {
            _renderer = renderer;
            _grid = grid;
            _camera = camera;
        }

        public void Render(Tile[][] tiles)
        {
            if (_grid == null)
                throw new NullReferenceException("Tile grid returned null, cannot render tiles.");

            if (_camera == null)
                throw new NullReferenceException("Camera returned null, cannot render tiles.");

            var visibleWorld = _camera.VisibleWorldBounds;

            var min = _grid.WorldPositionToTile(
                visibleWorld.X, visibleWorld.Y
            );

            var max = _grid.WorldPositionToTile(
                visibleWorld.X + visibleWorld.Width,
                visibleWorld.Y + visibleWorld.Height
            );

            int tilesWidth = tiles?.Length ?? 0;
            if (tilesWidth == 0) return;
            int tilesHeight = tiles?[0]?.Length ?? 0;
            if (tilesHeight == 0) return;

            int startX = Math.Max(0, Math.Min(min.x, tilesWidth - 1));
            int endX = Math.Max(0, Math.Min(max.x, tilesWidth - 1));
            int startY = Math.Max(0, Math.Min(min.y, tilesHeight - 1));
            int endY = Math.Max(0, Math.Min(max.y, tilesHeight - 1));

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    var tile = tiles?[x][y];
                    if (tile == null) continue;

                    drawTile(tile, x, y);
                }
            }
        }

        private void drawTile(Tile tile, int x, int y)
        {
            if(tile.TextureHandle == null) return;
            if(tile.TextureHandle == IntPtr.Zero) return;

            var worldX = x * Constants.TileSize;
            var worldY = y * Constants.TileSize;

            var screenPos = _camera.WorldPositionToScreenPosition(worldX, worldY);
            var tileZoom = (int)(Constants.TileSize * _camera.Zoom);

            _renderer.DrawTexture(
                tile.TextureHandle.Value,
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