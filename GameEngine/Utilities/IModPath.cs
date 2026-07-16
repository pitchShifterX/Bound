namespace GameEngine.Utilities
{
    public interface IModPath
    {
        public string ModName { get; }
        public string EngineRoot { get; }
        public string ModRoot { get; }

        public string Assets { get; }
        public string Config { get; }
        public string Maps { get; }
        public string Logs { get; }

        public void EnsureDirectories();

        public string GetModPath(string path);
        public string GetAssetPath(string path);
        public string GetConfigPath(string path);
        public string GetMapsPath(string path);
        public string GetLogsPath(string path);
    }
}