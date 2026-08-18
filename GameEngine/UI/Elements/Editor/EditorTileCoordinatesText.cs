using GameEngine.MapEditor;
using GameEngine.UI.Event.Types;

namespace GameEngine.UI.Elements.Editor
{
    public class EditorTileCoordinatesText : UIText
    {
        public EditorTileCoordinatesText() :
            base("(0, 0)")
        {
        }

        protected override void OnContextAssigned()
        {
            base.OnContextAssigned();

            Subscribe<TileHoverEvent>(OnTileHover);
        }

        private void OnTileHover(TileHoverEvent e)
        {
            SetLabel($"({e.X}, {e.Y})");
        }
    }
}