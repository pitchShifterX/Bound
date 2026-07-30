using GameEngine.Graphics.Rendering;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.Rendering.Cameras;

namespace GameEngine.World.ECS.Systems
{
    public class SelectionCircleRenderSystem
    {
        public void DrawCircle(ECSService ecs, IRenderContext renderContext, ICameraView camera)
        {
            var entities = ecs.GetEntitiesWith<SelectionCircleComponent, TransformComponent>();

            foreach(var entity in entities)
            {
                ref var circle = ref ecs.GetComponent<SelectionCircleComponent>(entity);
                ref var transform = ref ecs.GetComponent<TransformComponent>(entity);

                var bounds = new Rectangle<float>(
                    transform.Position.x - circle.Radius,
                    transform.Position.y - circle.Radius,
                    circle.Radius * 2,
                    circle.Radius * 2
                );

                if(!camera.IsVisible(bounds))
                    continue;
                
                var position = camera.WorldPositionToScreenPosition(
                    transform.Position.x + circle.Offset.x,
                    transform.Position.y + circle.Offset.y
                );

                var radius = circle.Radius * camera.Zoom;

                renderContext.DrawEllipse(
                    position,
                    radius,
                    radius * 0.5f,
                    circle.Color
                );
            }
        }
    }
}