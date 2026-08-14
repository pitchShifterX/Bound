using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public abstract partial class UIElement<T> : IUIElement
        where T : UIElement<T>
    {
        /// <summary>
        /// References self of child element type.
        /// </summary>
        protected T Self => (T)this;

        /// <summary>
        /// The UI context passed down from parent element to child element. This 
        /// gives access to theme, rendering methods, etc.
        /// </summary>
        protected UIContext? Context { get; private set; }

        /// <summary>
        /// Possible parent element. Nullable as Root element does not have a 
        /// parent.
        /// </summary>
        public IUIElement? Parent { get; set; }

        /// <summary>
        /// All of the child nodes of this element.
        /// </summary>
        public List<IUIElement> Children { get; protected set; } = [];

        /// <summary>
        /// The resolved layout of our element after calculation. This 
        /// represents the actual area occupied by our element.
        /// </summary>
        public Rectangle<float> Bounds { get; private set; }

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
            = HorizontalAlignment.Left;

        /// <summary>
        /// The vertical alignment of our element. By default, top.
        /// </summary>
        public VerticalAlignment VerticalAlignment { get; set; }
            = VerticalAlignment.Top;
        
        /// <summary>
        /// The available area for child elements to use. This takes 
        /// into account padding.
        /// </summary>
        public Rectangle<float> ContentBounds
        {
            get
            {
                return new Rectangle<float>
                {
                    X = Bounds.X + Padding.Left,
                    Y = Bounds.Y + Padding.Top,
                    Width = MathF.Max(
                        Bounds.Width - Padding.Left - Padding.Right,
                        0
                    ),
                    Height = MathF.Max(
                        Bounds.Height - Padding.Top - Padding.Bottom,
                        0
                    )
                };
            }
        }

        /// <summary>
        /// Gets the current element's content boundaries.
        /// </summary>
        /// <returns></returns>
        public virtual Rectangle<float> GetContentBounds()
        {
            return new Rectangle<float>
            {
                X = Bounds.X + Padding.Left,
                Y = Bounds.Y + Padding.Top,
                Width = MathF.Max(
                    Bounds.Width - Padding.Left - Padding.Right,
                    0
                ),
                Height = MathF.Max(
                    Bounds.Height - Padding.Top - Padding.Bottom,
                    0
                )
            };
        }

        /// <summary>
        /// Currently unused.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Determines if the current element is enabled for interaction.
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Determines if the current element is visible.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Simply sets the horizontal and vertical alignment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T SetAlignment(HorizontalAlignment x, VerticalAlignment y)
        {
            HorizontalAlignment = x;
            VerticalAlignment = y;

            return Self;
        }
    }
}