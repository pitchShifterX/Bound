using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIText : UIElement
    {
        public string Label { get; set; }
        public Font? Font { get; set; }
        public Color? LabelColor { get; set; }

        public UIText(string label) :
            base(new Auto(), new Auto())
        {
            Label = label;
        }

        protected override void OnContextAssigned()
        {
            Font ??= Context!.Scene.GetById<Font>(Context!.Theme.FontResource);
            LabelColor ??= Context!.Theme.Buttons.LabelColor;
        }

        public UIText SetFont(Font font)
        {
            Font = font;

            return this;
        }

        public UIText SetColor(Color color)
        {
            LabelColor = color;

            return this;
        }

        public UIText SetMargin(UISpacing spacing)
        {
            Margin = spacing;

            return this;
        }

        public UIText SetPadding(UISpacing spacing)
        {
            Padding = spacing;

            return this;
        }

        public override bool Process(UIInput input)
        {
            return false;
        }

        public override void Render()
        {
            if(Context?.Render == null) return;
            if(Font == null) return;

            Context.Render.DrawText(
                Font,
                Label,
                LabelColor!.Value,
                new Vector2<float>(
                    Bounds.X,
                    Bounds.Y
                )
            );
        }

        protected override Vector2<float>? GetDesiredSize()
        {
            if(Font == null)
                return null;
            
            return Font.CalculateSize(Label).To<float>();
        }
    }
}