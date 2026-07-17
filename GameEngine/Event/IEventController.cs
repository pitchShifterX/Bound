namespace GameEngine.Event
{
    public interface IEventController
    {
        public bool IsQuitting { get; set; }
        public void PollEvents();
    }
}