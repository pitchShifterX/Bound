namespace GameEngine.Event
{
    public interface IReceiveEvents
    {
        public void BeginFrame();
        public void HandleEvent(EngineEvent e);
    }
}