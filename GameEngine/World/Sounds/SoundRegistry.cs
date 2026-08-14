using System.Diagnostics.CodeAnalysis;

namespace GameEngine.World.Sounds
{
    public class SoundRegistry
    {
        private readonly Dictionary<string, ISound> _sounds = [];

        public IReadOnlyDictionary<string, ISound> Library => _sounds;

        public void Register(ISound sound)
        {
            _sounds[sound.Id] = sound;
        }

        public bool Get(string soundId, [NotNullWhen(true)] out ISound? sound)
        {
            return _sounds.TryGetValue(soundId, out sound);
        }
    }
}