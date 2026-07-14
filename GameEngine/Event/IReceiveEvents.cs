using GameEngine.SharedInterface;

namespace GameEngine.Event
{
    public interface IReceiveEvents : IFrameLifecycle
    {
        public void HandleEvent(EngineEvent e);
    }
}