namespace GameEngine.World.Map.Triggers
{
    public interface ITriggerAction
    {
        public TriggerActionResult Execute(IGameplayContext context, float? delta);

        /// <summary>
        /// Only necessary when your action is preserved and data needs to reset.
        /// </summary>
        public void Reset() {}
    }
}