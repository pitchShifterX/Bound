namespace GameEngine.Exception
{
    public class SceneNotFoundException : System.Exception
    {
        public string RequestedId { get; private set; }

        public SceneNotFoundException(string id) :
            base($"Scene not found: {id}")
        {
            RequestedId = id;
        }
    }
}