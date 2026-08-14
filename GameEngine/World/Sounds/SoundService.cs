using GameEngine.Audio;

namespace GameEngine.World.Sounds
{
    public class SoundService
    {
        private IControlMusic _controller;
        private SoundRegistry _registry;

        public SoundService(IControlMusic controller, SoundRegistry registry)
        {
            _controller = controller;
            _registry = registry;
        }

        public void PlayMusic(string id)
        {
            if(_registry.Get(id, out var song))
            {
                _controller.PlayMusic(song.Id);
            }
        }

        public void StopMusic()
        {
            _controller.StopMusic();
        }
    }
}