namespace GameEngine.World.Map.Triggers.Actions
{
    public class SetMusicAction : ITriggerAction
    {
        private string _musicId;
        private bool _isPlaying;

        public SetMusicAction(string musicId, bool on)
        {
            _musicId = musicId;
            _isPlaying = on;
        }

        public TriggerActionResult Execute(IGameplayContext context, float? delta)
        {
            // audio service
            if(_isPlaying)
                context.Sound.PlayMusic(_musicId);
            else
                context.Sound.StopMusic();

            return TriggerActionResult.Completed;
        }
    }
}