using GameEngine.Graphics.Rendering;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Rendering.Cameras;
using SDL2;

namespace GameEngine.World.ECS.Systems
{
    public class RenderTextureSystem
    {
        public void DrawTextures(ECSService service, IRenderContext sceneContext, ICameraView camera)
        {
            var storedEntities = service.GetEntitiesWith<SpriteComponent, TransformComponent>();

            foreach(var entityId in storedEntities)
            {
                ref var transformComponent = ref service.GetComponent<TransformComponent>(entityId);
                ref var spriteComponent = ref service.GetComponent<SpriteComponent>(entityId);

                var position = transformComponent.Position;

                // might be able to optimize
                var texture = sceneContext.GetById<Texture>(spriteComponent.TextureId);

                var worldPosition = camera.WorldPositionToScreenPosition(position.x, position.y);
                var textureSize = (int)(Constants.TileSize * camera.Zoom);

                var originX = spriteComponent.Origin.x * camera.Zoom;
                var originY = spriteComponent.Origin.y * camera.Zoom;

                var absoluteX = (int)(worldPosition.x - originX);
                var absoluteY = (int)(worldPosition.y - originY);

                if(texture == null) continue;

                var srcRect = new SDL.SDL_Rect
                {
                    x = spriteComponent.SourceRectangle.X,
                    y = spriteComponent.SourceRectangle.Y,
                    w = spriteComponent.SourceRectangle.Width,
                    h = spriteComponent.SourceRectangle.Height
                };

                sceneContext.DrawTexture(
                    texture,
                    srcRect,
                    new SDL.SDL_Rect { x = absoluteX, y = absoluteY, w = textureSize, h = textureSize }
                );
            }
        }

        public void DrawBorder(ECSService service, ISceneContext sceneContext, ICameraView camera)
        {
            var storedEntities = service.GetEntitiesWith<BorderRenderComponent, Bounds2DComponent>();

            foreach(var entityId in storedEntities)
            {
                ref var borderComponent = ref service.GetComponent<BorderRenderComponent>(entityId);
                ref var rectComponent = ref service.GetComponent<Bounds2DComponent>(entityId);

                var screenPosition = camera.WorldPositionToScreenPosition(rectComponent.LocalBounds.X, rectComponent.LocalBounds.Y);
                
                int scaledWidth = (int)(rectComponent.LocalBounds.Width * camera.Zoom);
                int scaledHeight = (int)(rectComponent.LocalBounds.Height * camera.Zoom);

                var adjustedRect = new Rectangle<float>
                {
                    X = screenPosition.x,
                    Y = screenPosition.y,
                    Width = scaledWidth,
                    Height = scaledHeight
                };

                sceneContext.DrawRectangle(
                    adjustedRect,
                    borderComponent.BorderColor
                );
            }
        }
    }
}