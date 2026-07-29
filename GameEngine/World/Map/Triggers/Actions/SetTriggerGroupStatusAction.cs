namespace GameEngine.World.Map.Triggers.Actions
{
    public class SetTriggerGroupStatusAction : ITriggerAction
    {
        private string _triggerGroupId;
        private bool _status;

        public SetTriggerGroupStatusAction(string triggerGroupId, bool status)
        {
            _triggerGroupId = triggerGroupId;
            _status = status;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            var triggerGroup = context.TriggerEngine.GetTriggerGroupByName(_triggerGroupId);

            if(triggerGroup != null)
            {
                triggerGroup.IsEnabled = _status;
            }

            return TriggerActionResult.Completed;
        }
    }
}