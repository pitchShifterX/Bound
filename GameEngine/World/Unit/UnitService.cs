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
                new UnitComponent(prefabName)
            );

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

        public IEnumerable<int>? GetUnitsAtLocation(
            string unitName,
            string playerId,
            string locationId
        )
        {
            var entities = _ecs.GetEntitiesWith<UnitComponent, TransformComponent>();
            var location = _location.GetWorldBoundsByLocationName(locationId);
            var units = new List<int>();

            if(location == null)
            {
                Log.Warn($"Cannot get units {unitName} for player {playerId}. Location {locationId} not found.");

                return null;
            }

            foreach(var entity in entities)
            {
                ref var unit = ref _ecs.GetComponent<UnitComponent>(entity);
                ref var transform = ref _ecs.GetComponent<TransformComponent>(entity);
                ref var player = ref _ecs.GetComponent<PlayerOwnerComponent>(entity);

                if(unit.PrefabName != unitName)
                    continue;

                if(player.PlayerOwnerId != playerId)
                    continue;
                
                if(!location.Value.Contains(transform.Position))
                    continue;

                units.Add(entity);
            }

            return units;
        }

        public void KillAllUnitsAtLocation(
            string prefabName,
            string playerId,
            string locationId
        )
        {
            var units = GetUnitsAtLocation(prefabName, playerId, locationId);

            if(units == null) return;

            foreach(var unit in units)
            {
                var entityHandle = _ecs.GetEntityHandle(unit);

                if(entityHandle == null)
                {
                    Log.Warn($"Could not get entity handle for entity id {unit} using {prefabName}");

                    return;
                }

                _ecs.DestroyEntity(entityHandle.Value);

                Log.Info($"Killed unit {prefabName} with entity id {unit} at location {locationId}");
            }
        }
    }
}