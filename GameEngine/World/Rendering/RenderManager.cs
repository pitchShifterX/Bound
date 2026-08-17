using GameEngine.Graphics.Rendering;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Rendering.Cameras;
using GameEngine.World.Rendering.Tiles;

namespace GameEngine.World.Rendering
{
    public class RenderManager : IRenderable
    {
        private IMapView _map { get; init; }
        private ECSService _ecs { get; init; }
        private IRenderContext _draw { get; init; }
        private ICameraView _camera { get; init; }
        private SelectionService _selection { get; init; }
        private TilesetRegistry _tilesetRegistry { get; init; }

        public TileRenderer Tiles { get; set; }

        public RenderTextureSystem RenderTextureSystem { get; init; }
        public BorderRenderSystem BorderRenderSystem { get; set; }
        public SelectionCircleRenderSystem SelectionCircleRenderSystem { get; set; }
        public SelectionBoxRenderSystem SelectionBoxRenderSystem { get; set; }

        public RenderManager(
            IMapView map,
            ECSService ecs,
            IRenderContext draw,
            ICameraView camera,
            SelectionService selection,
            TilesetRegistry tilesets
        )
        {
            _map = map;
            _ecs = ecs;
            _draw = draw;
            _camera = camera;
            _selection = selection;
            _tilesetRegistry = tilesets;

            Tiles = new TileRenderer(
                _map,
                _draw,
                _camera,
                _tilesetRegistry
            );

            SelectionCircleRenderSystem = new SelectionCircleRenderSystem();
            SelectionBoxRenderSystem = new SelectionBoxRenderSystem();

            RenderTextureSystem = new RenderTextureSystem();

            BorderRenderSystem = new BorderRenderSystem(
                _ecs,
                _draw,
                _camera
            );
        }
        
        public void Render()
        {
            Tiles.Render();

            SelectionCircleRenderSystem.DrawCircle(_ecs, _draw, _camera);

            RenderTextureSystem.DrawTextures(_ecs, _draw, _camera);

            BorderRenderSystem.Render();

            SelectionBoxRenderSystem.Draw(_selection, _draw);
        }
    }
}