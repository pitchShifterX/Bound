using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Gameplay;
using GameEngine.World.ECS.Entities;

namespace GameEngine.World.Unit
{
    public class UnitService
    {
        private ECSService _ecs;
        private UnitPrefabRegistry _registry;

        public UnitService(ECSService service, UnitPrefabRegistry registry)
        {
            _ecs = service;
            _registry = registry;
        }

        public WorldEntityHandle? Create(
            string prefabName,
            string playerId,
            string locationId
        )
        {
            var prefab = _registry.Get(prefabName);

            if(prefab == null)
            {
                Log.Warn($"Could not create unit. Prefab does not exist for {prefabName}");

                return null;
            }

            var entity = _ecs.CreateEntity();
            prefab.CreatePrefab(entity.Id, _ecs);

            _ecs.AddComponent(
                entity.Id, 
                new PlayerOwnerComponent(playerId)
            );

            _ecs.AddComponent(
                entity.Id,
                new LocationComponent(locationId)
            );

            return entity;
        }
    }
}