using SDL2;

namespace GameEngine.Event
{
    public interface IEventController
    {
        public bool IsQuitting { get; }
        public void PollEvents();
    }
}