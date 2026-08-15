using GameEngine.UI.Event;

namespace GameEngine.UI.Elements
{
    public abstract partial class UIElement<T> : IUIElement
        where T : UIElement<T>
    {
        /// <summary>
        /// Actions stored when subscriptions are added. This lets 
        /// us unsubscribe to events added to child elements. So,
        /// when a parent element is unsubscribed, all child elements 
        /// are unsubscribed.
        /// </summary>
        private readonly List<Action> _unsubscribeActions = [];

        /// <summary>
        /// Called on SetContext() for elements to request 
        /// data from resources or theme. This also is the 
        /// place to subscribe to UI events.
        /// </summary>
        protected virtual void OnContextAssigned(){}

        /// <summary>
        /// Called when this element is being unloaded. Elements can 
        /// override this unload their own managed resources.
        /// </summary>
        protected virtual void OnUnsubscribe()
        {
        }

        /// <summary>
        /// Subscribe to a UI event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <exception cref="InvalidOperationException"></exception>
        protected void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : UIEvent
        {
            if(Context == null)
                throw new InvalidOperationException("Cannot subscribe to event. Missing context for UI element");

            Context.Events.Subscribe(handler);

            _unsubscribeActions.Add(
                () => Context.Events.Unsubscribe(handler)
            );
        }

        /// <summary>
        /// Call this method for the current element and child 
        /// elements to unsubscribe from events.
        /// </summary>
        public void Unsubscribe()
        {
            OnUnsubscribe();
            
            foreach(var unsubscribe in _unsubscribeActions)
                unsubscribe();

            _unsubscribeActions.Clear();

            foreach(var child in Children)
                child.Unsubscribe();
        }
    }
}