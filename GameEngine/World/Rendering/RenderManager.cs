using GameEngine.Graphics.Rendering;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Map;
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

        public TileRenderer Tiles { get; set; }
        public BorderRenderSystem BorderRenderSystem { get; set; }

        public RenderManager(
            IMapView map,
            ECSService ecs,
            IRenderContext draw,
            ICameraView camera
        )
        {
            _map = map;
            _ecs = ecs;
            _draw = draw;
            _camera = camera;

            Tiles = new TileRenderer(
                _map,
                _draw,
                _camera
            );

            BorderRenderSystem = new BorderRenderSystem(
                _ecs,
                _draw,
                _camera
            );
        }
        
        public void Render()
        {
            Tiles.Render();

            BorderRenderSystem.Render();
        }
    }
}