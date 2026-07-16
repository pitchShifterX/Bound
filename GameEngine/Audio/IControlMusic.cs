namespace GameEngine.Audio
{
    public interface IControlMusic
    {
        public void PlayMusic(string id, int loop = -1);
        public void ResumeMusic();
        public void PauseMusic();
        public void StopMusic();
    }
}