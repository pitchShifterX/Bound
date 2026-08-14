using GameEngine.UI.Input;
using GameEngine.UI.Properties;

namespace GameEngine.UI.Elements
{
    public abstract partial class UIElement<T> : IUIElement
        where T : UIElement<T>
    {
        /// <summary>
        /// Creates a UI element with a width and height. If not provided, 
        /// the element expands to fill its parent element.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected UIElement(UISize? width = null, UISize? height = null)
        {
            Width = width ?? new Fill();
            Height = height ?? new Fill();
        }

        /// <summary>
        /// Passes the UIContext and triggers OnContextAssigned() in 
        /// case elements need to grab resources/theme values.
        /// </summary>
        /// <param name="context"></param>
        public virtual void SetContext(UIContext context)
        {
            Context = context;

            OnContextAssigned();

            foreach(var child in Children)
                child.SetContext(context);
        }
        
        /// <summary>
        /// Currently unused until I need to update UI elements 
        /// e.g. text for displaying score or something.
        /// </summary>
        /// <param name="delta"></param>
        public virtual void Update(float? delta)
        {
        }

        /// <summary>
        /// Processes inputs. If the element returns true on this, 
        /// it is the higher priority of inputs. That means a 
        /// button on top of an element can be clicked if the 
        /// button's Process() returns true.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract bool Process(UIInput input);
        
        /// <summary>
        /// Renders all child elements.
        /// </summary>
        public virtual void Render()
        {
            foreach(var child in Children)
                child.Render();
        }
    }
}