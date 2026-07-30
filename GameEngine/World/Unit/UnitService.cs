using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;
using GameEngine.World.ECS.Entities;
using GameEngine.World.Map.Locations;

namespace GameEngine.World.Unit
{
    public class UnitService
    {
        private ECSService _ecs;
        private UnitPrefabRegistry _registry;
        private LocationService _location;

        public UnitService(ECSService service, UnitPrefabRegistry registry, LocationService location)
        {
            _ecs = service;
            _registry = registry;
            _location = location;
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

            var location = _location.GetWorldBoundsByLocationName(locationId);

            if(location == null)
            {
                Log.Warn($"Could not create unit. Location not found: {locationId}");

                return null;
            }

            _ecs.AddComponent(
                entity.Id, 
                new PlayerOwnerComponent(playerId)
            );

            _ecs.AddComponent(
                entity.Id,
                new TransformComponent
                {
                    Position = location.Value.Center
                }
            );

            Log.Info($"Unit {prefabName} created [{location.Value.Center.x}, {location.Value.Center.y}] with id {entity.Id}");

            return entity;
        }
    }
}