using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.Unit;

namespace Mods.Bound.Gameplay.Unit
{
    public class BounderPrefab : IUnitPrefab
    {
        public string Name => "Bounder";

        public void CreatePrefab(int entityId, ECSService service)
        {
            service.AddComponent(entityId, new SpriteComponent
            {
                TextureId = "runner" ,
                SourceRectangle = new Rectangle<int>(0, 0, 48, 48)
            });

            service.AddComponent(entityId, new TransformComponent {});
        }
    }
}