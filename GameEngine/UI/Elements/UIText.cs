using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Input;
using GameEngine.UI.Properties;
using GameEngine.Utilities;

namespace GameEngine.UI.Elements
{
    public class UIText : UIElement<UIText>
    {
        private string? _acquiredLabel;
        private bool _textTextureAcquired;

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

            getTextTexture();
        }

        public UIText SetLabel(string text)
        {
            if(Label == text)
                return Self;
            
            releaseTextTexture();

            Label = text;

            return Self;
        }

        public UIText SetFont(Font font)
        {
            if(Font == font)
                return Self;
            
            releaseTextTexture();

            Font = font;

            return Self;
        }

        public UIText SetColor(Color color)
        {
            if(LabelColor.HasValue && LabelColor.Value.Equals(color))
                return Self;

            releaseTextTexture();

            LabelColor = color;

            return Self;
        }

        public UIText SetMargin(UISpacing spacing)
        {
            Margin = spacing;

            return Self;
        }

        public UIText SetPadding(UISpacing spacing)
        {
            Padding = spacing;

            return Self;
        }

        public override bool Process(UIInput input)
        {
            return false;
        }

        public override void Render()
        {
            if(Context?.Render == null) return;
            if(Font == null) return;
            if(LabelColor == null) return;

            getTextTexture();

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

        protected override void OnUnsubscribe()
        {
            releaseTextTexture();
        }

        private void getTextTexture()
        {
            if(Context == null)
                return;

            if(Font == null)
                return;

            if(LabelColor == null)
                return;

            // stop if we are already using a reference to this exact text
            if(_textTextureAcquired && _acquiredLabel == Label)
                return;

            // safely release texture if label/font/color changed without 
            // using one of the methods
            releaseTextTexture();

            var color = LabelColor.Value;

            Context.Scene.GetTextTexture(
                Font,
                Label,
                color.ToSDL()
            );

            _acquiredLabel = Label;
            _textTextureAcquired = true;
        }

        private void releaseTextTexture()
        {
            if(!_textTextureAcquired)
                return;

            if(Context == null)
                return;

            if(Font == null)
                return;

            if(LabelColor == null)
                return;

            Context.Scene.ReleaseTextTexture(
                Font,
                _acquiredLabel ?? Label,
                LabelColor.Value.ToSDL()
            );

            _acquiredLabel = null;
            _textTextureAcquired = false;
        }
    }
}