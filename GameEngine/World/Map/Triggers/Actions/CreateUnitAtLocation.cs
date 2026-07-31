namespace GameEngine.World.Map.Triggers.Actions
{
    public class CreateUnitAtLocation : ITriggerAction
    {
        private string _unitName;
        private int _playerId;
        private string _locationId;

        public CreateUnitAtLocation(string unitName, int playerId, string locationId)
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