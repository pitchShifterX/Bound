using GameEngine.SharedInterface;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public interface IUIElement : IUpdatable, IRenderable
    {
        /// <summary>
        /// Possible parent element. Nullable as Root element does not have a 
        /// parent.
        /// </summary>
        public IUIElement? Parent { get; set; }

        /// <summary>
        /// All of the child nodes of this element.
        /// </summary>
        public List<IUIElement> Children { get; }

        /// <summary>
        /// The resolved layout of our element after calculation. This 
        /// represents the actual area occupied by our element.
        /// </summary>
        public Rectangle<float> Bounds { get; }

        /// <summary>
        /// The available area for child elements to use. This takes 
        /// into account padding.
        /// </summary>
        public Rectangle<float> ContentBounds { get; }

        /// <summary>
        /// Determines the fixed/filled/auto size width properties. 
        /// Auto is used with text, as it needs to calculate its 
        /// own width via SDL2_ttf.
        /// </summary>
        public UISize Width { get; init; }

        /// <summary>
        /// Determines the fixed/filled/auto size height properties. 
        /// AUto is used with text, as it needs to calculate its 
        /// own height via SDL2_ttf.
        /// </summary>
        public UISize Height { get; init; }

        /// <summary>
        /// Set spacing for this element.
        /// </summary>
        public UISpacing Margin { get; set; }

        /// <summary>
        /// Set spacing for child elements.
        /// </summary>
        public UISpacing Padding { get; set; }
        
        /// <summary>
        /// The horizontal alignment of our element. By default, left.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// The vertical alignment of our element. By default, top.
        /// </summary>
        public VerticalAlignment VerticalAlignment { get; set; }

        public bool IsEnabled { get; }
        public bool IsVisible { get; }

        /// <summary>
        /// Passes the UIContext and triggers OnContextAssigned() in 
        /// case elements need to grab resources/theme values.
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(UIContext context);

        /// <summary>
        /// Called after BuildUI() from UIManager to calculate the 
        /// full tree of our UI elements. This calls the calculations 
        /// for each element so they can rely on parental elements.
        /// </summary>
        public void Layout();

        /// <summary>
        /// Adds a child element to our element and sets its parent 
        /// node.
        /// </summary>
        /// <param name="element"></param>
        public void AddChild(IUIElement element);

        /// <summary>
        /// Simply calls AddChild() for an array of elements.
        /// </summary>
        /// <param name="elements"></param>
        public void AddChildren(IEnumerable<IUIElement> elements);

        /// <summary>
        /// Processes inputs. If the element returns true on this, 
        /// it is the higher priority of inputs. That means a 
        /// button on top of an element can be clicked if the 
        /// button's Process() returns true.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Process(UIInput input);

        /// <summary>
        /// Gets the current element's content boundaries.
        /// </summary>
        /// <returns></returns>
        public Rectangle<float> GetContentBounds();
    }
}