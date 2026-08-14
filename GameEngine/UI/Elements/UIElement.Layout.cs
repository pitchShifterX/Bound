using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public abstract partial class UIElement<T> : IUIElement
        where T : UIElement<T>
    {
        /// <summary>
        /// Called after BuildUI() from UIManager to calculate the 
        /// full tree of our UI elements. This calls the calculations 
        /// for each element so they can rely on parental elements.
        /// </summary>
        public virtual void Layout()
        {
            CalculateBounds();

            LayoutChildren();
        }

        /// <summary>
        /// Overload for Layout() to set the bounds of the current 
        /// element before laying out the child elements.
        /// </summary>
        /// <param name="bounds"></param>
        public virtual void Layout(Rectangle<float> bounds)
        {
            Bounds = bounds;

            LayoutChildren();
        }

        /// <summary>
        /// Called by Layout() to continue building out layouts for 
        /// child elements.
        /// </summary>
        public virtual void LayoutChildren()
        {
            foreach(var child in Children)
                child.Layout();
        }

        /// <summary>
        /// Simply sets the layout bounds.
        /// </summary>
        /// <param name="bounds"></param>
        public virtual void SetLayoutBounds(Rectangle<float> bounds)
        {
            Bounds = bounds;
        }

        /// <summary>
        /// Gets the desired size of this element for use by parent layout
        /// containers such as UIFlexBox.
        /// </summary>
        public Vector2<float> GetLayoutDesiredSize()
        {
            return GetDesiredSize() ?? new Vector2<float>(0, 0);
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