using System.Text.RegularExpressions;

namespace GameEngine.Utilities
{
    public class ModPath : IModPath
    {
        public string ModName { get; }
        public string EngineRoot { get; }
        public string ModRoot { get; }

        public string Assets { get; }
        public string Config { get; }
        public string Maps { get; }
        public string Logs { get; }

        public ModPath(string engineRoot, string modName)
        {
            ModName = modName;
            EngineRoot = engineRoot;

            var safeModName = sanitizeModName(modName);
            ModRoot = Path.Combine(EngineRoot, "mods", safeModName);

            Assets = Path.Combine(ModRoot, "assets");
            Config = Path.Combine(ModRoot, "config");
            Maps = Path.Combine(ModRoot, "maps");
            Logs = Path.Combine(ModRoot, "logs");
        }

        public static IModPath Create(string engineRoot, string modName)
        {
            return new ModPath(engineRoot, modName);
        }

        public void EnsureDirectories()
        {
            Directory.CreateDirectory(ModRoot);
            Directory.CreateDirectory(Assets);
            Directory.CreateDirectory(Config);
            Directory.CreateDirectory(Maps);
            Directory.CreateDirectory(Logs);
        }

        public string GetModPath(string path)
            => combineSafe(ModRoot, path);

        public string GetAssetPath(string path)
            => combineSafe(Assets, path);
        
        public string GetConfigPath(string path)
            => combineSafe(Config, path);

        public string GetMapsPath(string path)
            => combineSafe(Maps, path);

        public string GetLogsPath(string path)
            => combineSafe(Logs, path);

        private string combineSafe(string baseDirectory, string relative)
        {
            relative = relative.Replace(
                Path.AltDirectorySeparatorChar,
                Path.DirectorySeparatorChar
            );

            if(Path.IsPathRooted(relative))
                throw new ArgumentException("Relative path must not be root", nameof(relative));
            
            var combined = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            var baseFull = Path.GetFullPath(baseDirectory).TrimEnd(Path.DirectorySeparatorChar) + 
                Path.DirectorySeparatorChar;
            
            if(!combined.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase))
                throw new UnauthorizedAccessException("Relative path escapes the base directory");
            
            return combined;
        }

        private string sanitizeModName(string modName)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var cleaned = new string(
                modName.Where(m => !invalid.Contains(m) && !char.IsControl(m))
                    .ToArray()
            ).Trim();

            cleaned = Regex.Replace(cleaned, @"\s+", "_");

            return cleaned;
        }
    }
}