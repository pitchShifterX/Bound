namespace GameEngine.World.Map.Triggers.Actions
{
    public class CreateUnitAtLocation : ITriggerAction
    {
        private string _unitName;
        private string _playerId;
        private string _locationId;

        public CreateUnitAtLocation(string unitName, string playerId, string locationId)
        {
            _unitName = unitName;
            _playerId = playerId;
            _locationId = locationId;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            context.Unit.Create(_unitName, _playerId, _locationId);

            return TriggerActionResult.Completed;
        }
    }
}