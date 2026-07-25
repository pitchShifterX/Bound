using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;
using GameEngine.World.Unit;
using MoonSharp.Interpreter;

namespace GameEngine.World.Map
{
    /// <summary>
    /// Will be deleted once I finish writing a trigger system to replace it
    /// </summary>
    [MoonSharpUserData]
    public class MapAPI
    {
        private readonly ECSService _ecs;
        private readonly UnitPrefabRegistry _unitRegistry;

        public MapAPI(ECSService ecs, UnitPrefabRegistry unitRegistry)
        {
            _ecs = ecs;
            _unitRegistry = unitRegistry;
        }

        public void SpawnUnit(string prefabName, string playerId, float x, float y)
        {
            var prefab = _unitRegistry.Get(prefabName);

            if(prefab == null)
            {
                Log.Warn($"Could not spawn unit. Prefab {prefabName} not found in registry.");

                return;
            }

            var entityHandle = _ecs.CreateEntity();
            prefab.CreatePrefab(entityHandle.Id, _ecs);

            _ecs.AddComponent(entityHandle.Id, new TransformComponent { Position = new Vector2<float>{ x = x, y = y }});
            _ecs.AddComponent(entityHandle.Id, new PlayerOwnerComponent { PlayerOwnerId = playerId });

            Log.Info($"Spawned unit {prefabName} for {playerId} @ [{x}, {y}]");
        }

        public void KillUnitForPlayer(string prefabName, string playerId, float x, float y, float w, float h)
        {
            var prefab = _unitRegistry.Get(prefabName);

            if(prefab == null)
            {
                Log.Warn($"Could not kill unit. Prefab {prefabName} not found in registry.");
            }

            var entities = _ecs.GetEntitiesWith<TransformComponent, PlayerOwnerComponent>();

            foreach(var entityId in entities)
            {
                ref var transformComponent = ref _ecs.GetComponent<TransformComponent>(entityId);
                ref var playerComponent = ref _ecs.GetComponent<PlayerOwnerComponent>(entityId);

                if(playerComponent.PlayerOwnerId != playerId)
                    continue;
                
                var coords = transformComponent.Position;

                if(coords.x >= x && coords.x <= coords.x + w &&
                    coords.y >= y && coords.y <= coords.y + h)
                {
                    var handle = _ecs.GetEntityHandle(entityId);

                    if(handle != null && handle.HasValue)
                    {
                        _ecs.DestroyEntity(handle.Value);

                        Log.Info($"Killed unit {prefabName} for {playerId}.");
                    }
                }
            }
        }
    }
}