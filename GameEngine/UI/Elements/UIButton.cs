using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIButton : UIElement
    {
        public Action? Action { get; init; }

        public bool IsHovered { get; private set; }
        public bool IsPressed { get; private set; }

        public string? Label { get; set; }
        public Font? Font { get; set; }
        public Color? LabelColor { get; set; }
        public Color? BorderColor { get; set; }

        public UIButton(Rectangle<float> rect, Action? action) : 
            base(rect)
        {
            Rectangle = rect;
            Action = action;
        }

        protected override void OnContextAssigned()
        {
            Font ??= Context!.Scene.GetById<Font>(Context!.Theme.FontResource);
            LabelColor ??= Context!.Theme.Buttons.LabelColor;
            BorderColor ??= Context!.Theme.Buttons.BorderColor;
        }

        public override bool Process(UIInput input)
        {
            IsHovered = Bounds.Contains(input.MousePosition);

            if(!IsHovered)
                return false;

            if(input.MouseLeftPressed)
            {
                IsPressed = true;

                return true;
            }
            
            if(IsPressed && input.MouseLeftReleased)
            {
                IsPressed = false;
                Action?.Invoke();

                return true;
            }

            return true;
        }

        public override void Update(float? delta)
        {
            base.Update(delta);
        }

        public override void Render()
        {
            if(Context?.Render == null) return;

            Context.Render.DrawRectangle(Bounds, BorderColor!.Value);

            if(Label == null || Font == null) return;

            var textSize = Font.CalculateSize(Label);

            var textPosition = new Vector2<float>(
                Center.x - textSize.x / 2f,
                Center.y - textSize.y / 2f
            );

            Context.Render.DrawText(
                Font,
                Label,
                LabelColor!.Value,
                textPosition
            );
        }
    }
}