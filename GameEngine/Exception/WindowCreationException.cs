namespace GameEngine.Exception
{
    public class WindowCreationException : System.Exception
    {
        public WindowCreationException(string message) :
            base($"Window could not be created: {message}")
        {
        }
    }
}