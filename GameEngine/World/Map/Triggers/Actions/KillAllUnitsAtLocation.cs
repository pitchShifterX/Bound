namespace GameEngine.World.Map.Triggers.Actions
{
    public class KillAllUnitsAtLocation : ITriggerAction
    {
        private string _unitName;
        private string _playerId;
        private string _locationId;

        public KillAllUnitsAtLocation(
            string unitName,
            string playerId,
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