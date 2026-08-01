using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;

namespace GameEngine.World.Map.Triggers.Conditions
{
    public class PlayerBringsUnitToLocationCondition : ITriggerCondition
    {
        private int _playerId;
        private string _unitName;
        private string _locationName;

        public PlayerBringsUnitToLocationCondition(
            int playerId,
            string unitName,
            string locationName
        )
        {
            _playerId = playerId;
            _unitName = unitName;
            _locationName = locationName;
        }

        public bool Evaluate(IGameplayContext context)
        {
            var ecs = context.ECS;
            var entities = ecs.GetEntitiesWith<UnitComponent, TransformComponent, PlayerOwnerComponent>();

            foreach(var entity in entities)
            {
                ref var player = ref ecs.GetComponent<PlayerOwnerComponent>(entity);
                ref var unit = ref ecs.GetComponent<UnitComponent>(entity);
                ref var transform = ref ecs.GetComponent<TransformComponent>(entity);
                
                if(player.PlayerOwnerId != _playerId)
                    continue;

                if(unit.PrefabName != _unitName)
                    continue;

                if(context.Location.Contains(_locationName, transform.Position))
                    return true;
            }

            return false;
        }
    }
}