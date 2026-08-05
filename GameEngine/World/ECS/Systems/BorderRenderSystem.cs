using GameEngine.Graphics.Rendering;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
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
            var entities = _ecs.GetEntitiesWith<Bounds2DComponent, BorderRenderComponent>();

            foreach (var entity in entities)
            {
                ref var bounds = ref _ecs.GetComponent<Bounds2DComponent>(entity);
                ref var border = ref _ecs.GetComponent<BorderRenderComponent>(entity);

                Rectangle<float> worldRect;

                // renders entities borders with transform components and also locations (which do not)
                if (_ecs.HasComponent<TransformComponent>(entity))
                {
                    ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);

                    worldRect = new Rectangle<float>
                    {
                        X = transform.Position.x + bounds.LocalBounds.X,
                        Y = transform.Position.y + bounds.LocalBounds.Y,
                        Width = bounds.LocalBounds.Width,
                        Height = bounds.LocalBounds.Height
                    };
                }
                else
                {
                    worldRect = bounds.LocalBounds;
                }

                if (!_camera.IsVisible(worldRect))
                    continue;

                var screenRect = _camera.WorldToViewportRectangle(worldRect);

                _renderer.DrawRectangle(
                    screenRect,
                    border.BorderColor
                );
            }
        }
    }
}