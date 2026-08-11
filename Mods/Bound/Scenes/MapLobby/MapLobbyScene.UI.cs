using GameEngine.Graphics.Primitives;
using GameEngine.UI.Elements;
using GameEngine.UI.Properties;

namespace Mods.Bound.Scenes.Gameplay
{
    public partial class MapLobbyScene
    {
        public override void BuildUI()
        {
            var header = buildHeader();

            UI.Root.AddChild(header);
            UI.Root.SetPadding(new(0, 100, 0, 100));
        }

        private IUIElement buildHeader()
        {
            var header = new UIPanel(new Fill(), new Fixed(150))
                .SetPadding(new(25, 0, 0, 0))
                .SetBorderColor(Color.Green);

            var title = new UIText("Map Lobby")
                .SetColor(Color.White)
                .SetFont(_default!);

            var subtitle = new UIText("Single Player")
                .SetFont(_interExtraLight!)
                .SetMargin(new(50, 0, 0, 0))
                .SetColor(Color.White);

            header.AddChild(title);
            header.AddChild(subtitle);

            return header;
        }
    }
}