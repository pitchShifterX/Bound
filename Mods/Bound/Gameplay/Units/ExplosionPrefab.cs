using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Unit;

namespace Mods.Bound.Gameplay.Unit
{
    public class ExplosionPrefab : IUnitPrefab
    {
        public string Name => "Explosion2x2";
        public string TexturePath => "textures/explosion.png";

        public void CreatePrefab(int entityId, ECSService service)
        {
            service.AddComponent(entityId, new SpriteComponent
            {
                TextureId = Name,
                SourceRectangle = new(0, 0, 64, 64),
                Size = new(64, 64),
                Origin = new(32, 32)
            });

            service.AddComponent(entityId, new Bounds2DComponent
            {
                LocalBounds =
                {
                    X = -32,
                    Y = -32,
                    Width = 64,
                    Height = 64
                }
            });

            service.AddComponent(entityId, new CollisionComponent
            {
                Layer = CollisionLayer.Explosion,
                Mask = CollisionLayer.GroundUnit
            });

            service.AddComponent(entityId, new TransformComponent{});
        }
    }
}