using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public abstract class UIElement : IUIElement
    {
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

        public string? Id { get; set; }
        public bool IsEnabled { get; set; } = true;
        public bool IsVisible { get; set; } = true;

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
        /// Called after BuildUI() from UIManager to calculate the 
        /// full tree of our UI elements. This calls the calculations 
        /// for each element so they can rely on parental elements.
        /// </summary>
        public virtual void Layout()
        {
            CalculateBounds();

            foreach(var child in Children)
                child.Layout();
        }

        /// <summary>
        /// Adds a child element to our element and sets its parent 
        /// node.
        /// </summary>
        /// <param name="element"></param>
        public void AddChild(IUIElement element)
        {
            element.Parent = this;

            if(Context != null)
                element.SetContext(Context);

            Children.Add(element);
        }

        /// <summary>
        /// Simply calls AddChild() for an array of elements.
        /// </summary>
        /// <param name="elements"></param>
        public void AddChildren(IEnumerable<IUIElement> elements)
        {
            foreach(var element in elements)
            {
                AddChild(element);
            }
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

        /// <summary>
        /// Called on SetContext() for elements to request 
        /// data from resources or theme.
        /// </summary>
        protected virtual void OnContextAssigned(){}

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
        /// Calculates the root boundaries.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual Rectangle<float> CalculateRootBounds()
        {
            Bounds = new Rectangle<float>
            {
                X = 0,
                Y = 0,
                Width = Width switch
                {
                    Fixed fixedSize => fixedSize.Value,
                    Auto => GetDesiredSize()?.x ?? 0,
                    Fill => 0,
                    _ => throw new InvalidOperationException("Unsupported width type")
                },
                Height = Height switch
                {
                    Fixed fixedSize => fixedSize.Value,
                    Auto => GetDesiredSize()?.y ?? 0,
                    Fill => 0,
                    _ => throw new InvalidOperationException("Unsupported height type")
                }
            };

            return Bounds;
        }

        /// <summary>
        /// Calculates the bounds of our element. This factors in 
        /// things like padding, margin, alignment, etc. Once done, 
        /// the Bounds property is set with the elements position 
        /// and size on the screen.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        protected virtual void CalculateBounds()
        {
            if(Parent == null)
            {
                CalculateRootBounds();

                return;
            }

            var parentContentBounds = Parent.GetContentBounds();
            var availableWidth = MathF.Max(
                parentContentBounds.Width - Margin.Left - Margin.Right,
                0
            );
            var availableHeight = MathF.Max(
                parentContentBounds.Height - Margin.Top - Margin.Bottom,
                0
            );

            // used for elements like UIText which needs to calculate their size 
            var desiredSize = GetDesiredSize();

            var width = Width switch
            {
                Fixed fixedSize => fixedSize.Value,
                Fill => availableWidth,
                Auto => desiredSize?.x ?? 0,
                _ => throw new InvalidOperationException("Unsupported width type")
            };

            var height = Height switch
            {
                Fixed fixedSize => fixedSize.Value,
                Fill => availableHeight,
                Auto => desiredSize?.y ?? 0,
                _ => throw new InvalidOperationException("Unsupported height type")
            };

            width = MathF.Min(width, availableWidth);
            height = MathF.Min(height, availableHeight);

            var x = HorizontalAlignment switch
            {
                HorizontalAlignment.Left => parentContentBounds.X + Margin.Left,

                HorizontalAlignment.Center => 
                    parentContentBounds.X + Margin.Left + (availableWidth - width) / 2f,
                
                HorizontalAlignment.Right => 
                    parentContentBounds.X + Margin.Left + availableWidth - width,
                
                _ => throw new InvalidOperationException("Unsupported horizontal alignment")
            };

            var y = VerticalAlignment switch
            {
                VerticalAlignment.Top =>
                    parentContentBounds.Y + Margin.Top,

                VerticalAlignment.Center =>
                    parentContentBounds.Y + Margin.Top + (availableHeight - height) / 2f,

                VerticalAlignment.Bottom =>
                    parentContentBounds.Y + Margin.Top + availableHeight - height,

                _ => throw new InvalidOperationException("Unsupported vertical alignment")
            };

            Bounds = new Rectangle<float>
            {
                X = x,
                Y = y,
                Width = MathF.Min(width, availableWidth),
                Height = MathF.Min(height, availableHeight)
            };
        }

        /// <summary>
        /// Used when certain elements like UIText need to calculate their own size.
        /// </summary>
        /// <returns></returns>
        protected virtual Vector2<float>? GetDesiredSize()
        {
            return null;
        }
    }
}