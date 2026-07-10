using Serilog;

namespace GameEngine.Utilities
{
    public static class Log
    {
        static Log()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public static void Info(string message) =>
            Serilog.Log.Information(message);

        public static void Debug(string message) =>
            Serilog.Log.Debug(message);

        public static void Warn(string message) =>
            Serilog.Log.Warning(message);

        public static void Error(string message, System.Exception? exception = null)
        {
            if (exception != null)
                Serilog.Log.Error(exception, message);
            else
                Serilog.Log.Error(message);
        }
    }
}