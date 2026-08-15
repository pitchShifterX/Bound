using GameEngine.SharedInterface;
using GameEngine.UI;
using GameEngine.Utilities;

namespace GameEngine.UI.Event
{
    public class UIEventBus : ILoadable
    {
        private readonly Dictionary<Type, List<Delegate>> _listeners = [];

        public void Subscribe<T>(Action<T> handler) where T : UIEvent
        {
            if(!_listeners.TryGetValue(typeof(T), out var handlers))
            {
                handlers = new List<Delegate>();
                _listeners[typeof(T)] = handlers;
            }

            handlers.Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : UIEvent
        {
            if(!_listeners.TryGetValue(typeof(T), out var handlers))
                return;

            handlers.Remove(handler);

            if(handlers.Count == 0)
                _listeners.Remove(typeof(T));
        }

        public void Publish<T>(T e) where T : UIEvent
        {
            if(!_listeners.TryGetValue(typeof(T), out var handlers))
                return;

            foreach(var handler in handlers.ToArray())
            {
                ((Action<T>)handler)(e);
            }
        }
        
        public void Load(){}

        /// <summary>
        /// Called when scene unloads resources.
        /// </summary>
        public void Unload()
        {
            Log.Debug("Unloaded listeners from UI Event bus");

            _listeners.Clear();
        }
    }
}