using System;
using System.Text.Json;

namespace GameEngine.Utilities
{
    public class Json<T> where T : new()
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        public string FilePath { get; private set; }

        public T? Value { get; private set; }

        public Json(string path)
        {
            var exePath = AppContext.BaseDirectory;

            FilePath = Path.Combine(exePath, path);
        }

        public bool TryLoad()
        {
            if(!File.Exists(FilePath))
            {
                Log.Info($"JSON file not found: ${FilePath} -- writing new file.");

                Value = new T();
                TryWrite();
                return false;
            }

            try
            {
                var json = File.ReadAllText(FilePath);

                Value = JsonSerializer.Deserialize<T>(json, _options) ?? new T();
                return true;
            }
            catch(System.Exception e)
            {
                Log.Error($"Failed to load JSON '{FilePath}': {e}");

                Value = new T();
                return false;
            }
        }

        public bool TryWrite()
        {
            if(Value == null)
            {
                Log.Warn("Cannot write JSON because value is null");
                return false;
            }

            try
            {
                var directory = Path.GetDirectoryName(FilePath);

                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                var json = JsonSerializer.Serialize(Value, _options);

                File.WriteAllText(FilePath, json);
                return true;
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to write JSON '{FilePath}': {ex}");
                return false;
            }
        }
    }
}