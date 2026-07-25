using GameEngine.Graphics.Cameras;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using SDL2;

namespace GameEngine.World.ECS.Systems
{
    public class RenderTextureSystem
    {
        public void DrawTextures(ECSService service, ISceneContext sceneContext, ICameraView camera)
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
                    new SDL2.SDL.SDL_Rect { x = worldPosition.x, y = worldPosition.y, w = textureSize, h = textureSize }
                );
            }
        }

        public void DrawBorder(ECSService service, ISceneContext sceneContext, ICameraView camera)
        {
            var storedEntities = service.GetEntitiesWith<BorderRenderComponent, Rectangle2DComponent>();

            foreach(var entityId in storedEntities)
            {
                ref var borderComponent = ref service.GetComponent<BorderRenderComponent>(entityId);
                ref var rectComponent = ref service.GetComponent<Rectangle2DComponent>(entityId);

                var screenPosition = camera.WorldPositionToScreenPosition(rectComponent.Value.X, rectComponent.Value.Y);
                
                int scaledWidth = (int)(rectComponent.Value.Width * camera.Zoom);
                int scaledHeight = (int)(rectComponent.Value.Height * camera.Zoom);

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