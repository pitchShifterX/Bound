using GameEngine.World.ECS.Components.Core;
using GameEngine.World.ECS.Components.Gameplay;

namespace GameEngine.World.ECS.Systems
{
    public class SelectionSystem
    {
        public void HandleClick(ECSService service, float clickWorldX, float clickWorldY, string localPlayerId)
        {
            removeExistingSelection(service, localPlayerId);

            var position = service.GetStorage<TransformComponent>();

            foreach(var keyValuePair in position)
            {
                var entityId = keyValuePair.Key;
                var pos = keyValuePair.Value;

                if (MathF.Abs(pos.Position.x - clickWorldX) < 16 && MathF.Abs(pos.Position.y - clickWorldY) < 16)
                {
                    service.AddComponent(entityId, new SelectedUnitByPlayerComponent { PlayerId = localPlayerId });
                    break; 
                }
            }
        }

        private void removeExistingSelection(ECSService service, string localPlayerId)
        {
            var selected = service.GetStorage<SelectedUnitByPlayerComponent>();

            List<int> previouslySelectedEntities = [];

            foreach(var keyValuePair in selected)
            {
                if(keyValuePair.Value.PlayerId == localPlayerId)
                {
                    previouslySelectedEntities.Add(keyValuePair.Key);
                }
            }

            foreach(var entityId in previouslySelectedEntities)
            {
                selected.Remove(entityId);
            }
        }
    }
}