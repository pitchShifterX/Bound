namespace GameEngine.World.Map.Triggers.Actions
{
    public class KillAllUnitsAtLocationAction : ITriggerAction
    {
        private string _unitName;
        private int _playerId;
        private string _locationId;

        public KillAllUnitsAtLocationAction(
            string unitName,
            int playerId,
            string locationId
        )
        {
            _unitName = unitName;
            _playerId = playerId;
            _locationId = locationId;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            context.Unit.KillAllUnitsAtLocation(_unitName, _playerId, _locationId);
            
            return TriggerActionResult.Completed;
        }
    }
}