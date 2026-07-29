using GameEngine.Graphics.Rendering;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.ECS.Systems
{
    public class BorderRenderSystem
    {
        private ECSService _ecs;
        private IRenderContext _renderer;
        private ICameraView _camera;

        public BorderRenderSystem(
            ECSService ecs,
            IRenderContext renderer,
            ICameraView camera
        )
        {
            _ecs = ecs;
            _renderer = renderer;
            _camera = camera;
        }

        public void Render()
        {
            var entities = _ecs.GetEntitiesWith<Rectangle2DComponent, BorderRenderComponent>();

            foreach(var entity in entities)
            {
                var rect = _ecs.GetComponent<Rectangle2DComponent>(entity);
                var border = _ecs.GetComponent<BorderRenderComponent>(entity);

                if(!_camera.IsVisible(rect.Value))
                    continue;

                var screenRect = _camera.WorldToViewportRectangle(rect.Value);

                _renderer.DrawRectangle(
                    screenRect,
                    border.BorderColor
                );
            }
        }
    }
}