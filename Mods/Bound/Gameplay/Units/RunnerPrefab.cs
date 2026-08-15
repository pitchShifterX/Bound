using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Unit;

namespace Mods.Bound.Gameplay.Unit
{
    public class RunnerPrefab : IUnitPrefab
    {
        public string Name => "Runner";
        public string TexturePath => "textures/runner.png";

        public void CreatePrefab(int entityId, ECSService service)
        {
            service.AddComponent(entityId, new SpriteComponent
            {
                TextureId = Name,
                SourceRectangle = new Rectangle<int>(0, 0, 48, 48),
                Size = new(32, 32),
                Origin = new(16, 32)
            });

            service.AddComponent(entityId, new Bounds2DComponent
            {
                LocalBounds =
                {
                    X = -12,
                    Y = -24,
                    Width = 24,
                    Height = 24
                }
            });

            service.AddComponent(entityId, new SelectionCircleSettingsComponent
            {
                Radius = 12,
                Offset = new Vector2<float>(0, -8)
            });

            service.AddComponent(entityId, new MovementSpeedComponent
            {
                Speed = 75
            });

            service.AddComponent(entityId, new CollisionComponent
            {
                Layer = CollisionLayer.GroundUnit,
                Mask = CollisionLayer.GroundUnit | CollisionLayer.Item | CollisionLayer.Explosion
            });

            service.AddComponent(entityId, new TransformComponent {});
            service.AddComponent(entityId, new MovementIntentComponent());
        }
    }
}