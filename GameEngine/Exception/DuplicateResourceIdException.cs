namespace GameEngine.Exception
{
    public class DuplicateResourceIdException : System.Exception
    {
        public DuplicateResourceIdException(string message) :
            base($"Duplicate resource: {message}")
        {
        }
    }
}