using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public enum FlexDirection
    {
        Row,
        Column
    }

    public enum FlexJustify
    {
        Start,
        Center,
        End,
        SpaceBetween,
        SpaceAround,
        SpaceEvenly
    }

    public enum FlexAlign
    {
        Start,
        Center,
        End,
        Stretch
    }

    public class UIFlexBox : AbstractContainerElement<UIFlexBox>
    {
        public FlexDirection Direction { get; set; } = FlexDirection.Row;

        public FlexAlign AlignItems { get; set; } = FlexAlign.Start;
        
        public FlexJustify JustifyContent { get; set; } = FlexJustify.Start;

        public float Gap { get; set; } = 0f;

        public UIFlexBox(UISize? width = null, UISize? height = null) :
            base(width, height)
        {
        }

        public UIFlexBox SetDirection(FlexDirection direction)
        {
            Direction = direction;

            return Self;
        }

        public UIFlexBox SetAlignItems(FlexAlign align)
        {
            AlignItems = align;
            
            return Self;
        }

        public UIFlexBox SetJustifyContent(FlexJustify justify)
        {
            JustifyContent = justify;

            return Self;
        }
        
        public UIFlexBox SetGap(float gap)
        {
            Gap = gap;

            return Self;
        }

        public override void Layout()
        {
            CalculateBounds();

            LayoutChildren();
        }

        public override void LayoutChildren()
        {
            var content = GetContentBounds();

            if(Direction == FlexDirection.Row)
                layoutRow(content);
            else
                layoutColumn(content);
        }

        public override bool Process(UIInput input)
        {
            foreach(var child in Children)
                child.Process(input);

            return false;
        }

        public override void Render()
        {
            Context?.Render.DrawRectangle(Bounds, BackgroundColor, BorderColor);

            base.Render();
        }

        protected override void OnContextAssigned()
        {
            BackgroundColor ??= Context!.Theme.FlexBoxes.BackgroundColor;
            BorderColor ??= Context!.Theme.FlexBoxes.BorderColor;
        }

        private void layoutRow(Rectangle<float> content)
        {
            if(Children.Count == 0)
                return;

            var sizes = new float[Children.Count];

            float totalFixedSize = 0f;
            int fillCount = 0;

            for(int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var desired = getChildDesiredSize(child);

                if(child.Width is Fill)
                {
                    fillCount++;
                }
                else
                {
                    var width = child.Width switch
                    {
                        Fixed fixedSize => fixedSize.Value,
                        Auto => desired.x,
                        _ => 0f
                    };

                    sizes[i] = width;

                    totalFixedSize += width;
                }

                totalFixedSize += child.Margin.Left;
                totalFixedSize += child.Margin.Right;
            }

            totalFixedSize += Gap * MathF.Max(Children.Count - 1, 0);

            var remainingSpace = MathF.Max(
                content.Width - totalFixedSize,
                0f
            );

            if(fillCount > 0)
            {
                var fillSize = remainingSpace / fillCount;

                for(int i = 0; i < Children.Count; i++)
                {
                    if(Children[i].Width is Fill)
                        sizes[i] = fillSize;
                }

                remainingSpace = 0f;
            }

            getJustifyValues(
                remainingSpace,
                out var startOffset,
                out var spacing
            );

            float x = content.X + startOffset;

            for(int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var width = sizes[i];

                x += child.Margin.Left;

                var height = getCrossAxisSize(
                    child,
                    content.Height
                );

                var y = getCrossAxisPosition(
                    child,
                    content.Y,
                    content.Height,
                    height
                );

                child.Layout(new Rectangle<float>
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                });

                x += width;
                x += child.Margin.Right;

                if(i < Children.Count - 1)
                    x += spacing;
            }
        }

        private void layoutColumn(Rectangle<float> content)
        {
            if (Children.Count == 0)
                return;

            var sizes = new float[Children.Count];

            float totalFixedSize = 0f;
            int fillCount = 0;

            for(int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var desired = getChildDesiredSize(child);

                if(child.Height is Fill)
                {
                    fillCount++;
                }
                else
                {
                    var height = child.Height switch
                    {
                        Fixed fixedSize => fixedSize.Value,
                        Auto => desired.y,
                        _ => 0f
                    };

                    sizes[i] = height;

                    totalFixedSize += height;
                }

                totalFixedSize += child.Margin.Top;
                totalFixedSize += child.Margin.Bottom;
            }

            totalFixedSize += Gap * MathF.Max(Children.Count - 1, 0);

            var remainingSpace = MathF.Max(
                content.Height - totalFixedSize,
                0f
            );

            if(fillCount > 0)
            {
                var fillSize = remainingSpace / fillCount;

                for(int i = 0; i < Children.Count; i++)
                {
                    if(Children[i].Height is Fill)
                        sizes[i] = fillSize;
                }

                remainingSpace = 0f;
            }

            getJustifyValues(
                remainingSpace,
                out var startOffset,
                out var spacing
            );

            float y = content.Y + startOffset;

            for(int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var height = sizes[i];

                y += child.Margin.Top;

                var width = getCrossAxisSize(
                    child,
                    content.Width
                );

                var x = getCrossAxisPosition(
                    child,
                    content.X,
                    content.Width,
                    width
                );

                child.Layout(new Rectangle<float>
                {
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                });

                y += height;
                y += child.Margin.Bottom;

                if (i < Children.Count - 1)
                    y += spacing;
            }
        }

        private Vector2<float> getChildDesiredSize(IUIElement child)
        {
            var desired = child.GetLayoutDesiredSize();

            var width = child.Width switch
            {
                Fixed fixedSize => fixedSize.Value,
                Auto => desired.x,
                Fill => 0f,
                _ => 0f
            };

            var height = child.Height switch
            {
                Fixed fixedSize => fixedSize.Value,
                Auto => desired.y,
                Fill => 0f,
                _ => 0f
            };

            return new Vector2<float>(width, height);
        }

        private float getCrossAxisSize(IUIElement child, float availableSize)
        {
            if(Direction == FlexDirection.Row)
            {
                if(AlignItems == FlexAlign.Stretch)
                {
                    return MathF.Max(
                        availableSize -
                        child.Margin.Top -
                        child.Margin.Bottom,
                        0f
                    );
                }

                return child.Height switch
                {
                    Fixed fixedSize => fixedSize.Value,
                    Auto => child.GetLayoutDesiredSize().y,
                    Fill => MathF.Max(
                        availableSize -
                        child.Margin.Top -
                        child.Margin.Bottom,
                        0f
                    ),
                    _ => 0f
                };
            }

            if(AlignItems == FlexAlign.Stretch)
            {
                return MathF.Max(
                    availableSize -
                    child.Margin.Left -
                    child.Margin.Right,
                    0f
                );
            }

            return child.Width switch
            {
                Fixed fixedSize => fixedSize.Value,
                Auto => child.GetLayoutDesiredSize().x,
                Fill => MathF.Max(
                    availableSize -
                    child.Margin.Left -
                    child.Margin.Right,
                    0f
                ),
                _ => 0f
            };
        }

        private float getCrossAxisPosition(
            IUIElement child,
            float start,
            float availableSize,
            float childSize
        )
        {
            if(Direction == FlexDirection.Row)
            {
                var available = MathF.Max(
                    availableSize -
                    child.Margin.Top -
                    child.Margin.Bottom,
                    0f
                );

                return AlignItems switch
                {
                    FlexAlign.Start =>
                        start + child.Margin.Top,

                    FlexAlign.Center =>
                        start +
                        child.Margin.Top +
                        (available - childSize) / 2f,

                    FlexAlign.End =>
                        start +
                        availableSize -
                        child.Margin.Bottom -
                        childSize,

                    FlexAlign.Stretch =>
                        start + child.Margin.Top,

                    _ => start
                };
            }

            var horizontalAvailable = MathF.Max(
                availableSize -
                child.Margin.Left -
                child.Margin.Right,
                0f
            );

            return AlignItems switch
            {
                FlexAlign.Start =>
                    start + child.Margin.Left,

                FlexAlign.Center =>
                    start +
                    child.Margin.Left +
                    (horizontalAvailable - childSize) / 2f,

                FlexAlign.End =>
                    start +
                    availableSize -
                    child.Margin.Right -
                    childSize,

                FlexAlign.Stretch =>
                    start + child.Margin.Left,

                _ => start
            };
        }

        private void getJustifyValues(float remainingSpace, out float startOffset, out float spacing)
        {
            switch (JustifyContent)
            {
                case FlexJustify.Start:
                    startOffset = 0f;
                    spacing = Gap;
                break;

                case FlexJustify.Center:
                    startOffset = remainingSpace / 2f;
                    spacing = Gap;
                break;

                case FlexJustify.End:
                    startOffset = remainingSpace;
                    spacing = Gap;
                break;

                case FlexJustify.SpaceBetween:
                    startOffset = 0f;

                    spacing = Children.Count > 1
                        ? Gap + remainingSpace / (Children.Count - 1)
                        : 0f;
                break;

                case FlexJustify.SpaceAround:
                    spacing =
                        Gap +
                        remainingSpace / Children.Count;

                    startOffset = spacing / 2f;
                break;

                case FlexJustify.SpaceEvenly:
                    spacing =
                        Gap +
                        remainingSpace / (Children.Count + 1);

                    startOffset = spacing;
                break;

                default:
                    startOffset = 0f;
                    spacing = Gap;
                break;
            }
        }
    }
}