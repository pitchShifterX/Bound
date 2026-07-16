using GameEngine.Resources;
using GameEngine.Settings;
using SDL2;

namespace GameEngine.Audio
{
    public class AudioManager : IAudioController
    {
        private ISettingsController _settings;
        private IResourceController _resources;

        public AudioManager(
            ISettingsController settings, 
            IResourceController resources
        )
        {
            _settings = settings;
            _resources = resources;

            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048) < 0)
                throw new System.Exception($"Could not initialize audio: {SDL.SDL_GetError()}");
        }

        public float MasterVolume
        {
            get => _settings.Settings.MasterVolume;
            set
            {
                var volumeClamp = Math.Clamp(value, 0f, 1f);

                _settings.UpdateMasterVolume(volumeClamp);
                SDL_mixer.Mix_VolumeMusic((int)(MasterVolume * SDL_mixer.MIX_MAX_VOLUME));
            }
        }

        /// <summary>
        /// <para>Play music.</para>
        /// <para>Automatically loops if loop count is -1</para>
        /// </summary>
        /// <param name="audioId"></param>
        /// <param name="loop">Amount of times to loop</param>
        public void PlayMusic(string audioId, int loop = -1)
        {
            var audio = _resources.GetById<Resources.Audio>(audioId);

            if(audio != null)
            {
                SDL_mixer.Mix_PlayMusic(audio.Handle, loop);
            }
        }

        public void ResumeMusic() => SDL_mixer.Mix_ResumeMusic();
        public void PauseMusic() => SDL_mixer.Mix_PauseMusic();
        public void StopMusic() => SDL_mixer.Mix_HaltMusic();

        public void Close() => SDL_mixer.Mix_CloseAudio();
    }
}