using GameEngine.Graphics.Primitives;
using GameEngine.Resources;
using GameEngine.UI.Event.Types;
using GameEngine.UI.Properties;
using GameEngine.Utilities;
using GameEngine.World;

namespace GameEngine.UI.Elements.Editor
{
    /// <summary>
    /// The toolbox is a flexbox containing tilesets and units. 
    /// <para>The toolbox subscribes to MapLoadEvent.</para>
    /// </summary>
    public class EditorToolbox : UIFlexBox
    {
        private IWorldContext _context;

        public EditorToolbox(IWorldContext context, UISize width, UISize height) :
            base(width, height)
        {
            _context = context;
            _context.UIEvents.Subscribe<MapLoadEvent>(OnMapLoad);
        }

        public class EditorTile
        {
            public string TilesetId { get; init; }
            public int Index { get; init; }
            public Rectangle<int> Source { get; init; }

            public EditorTile(string tilesetId, int index, Rectangle<int> source)
            {
                TilesetId = tilesetId;
                Index = index;
                Source = source;
            }
        }

        protected virtual void OnMapLoad(MapLoadEvent e)
        {
            var tiles = new List<EditorTile>();

            foreach(var tilesets in _context.Registries.Tilesets.Tiles)
            {
                var tileCount = tilesets.Value.TileDefinitions.Count();

                for(var tile = 0; tile < tileCount; tile++)
                {
                    var rect = tilesets.Value.GetSourceRectangle(tile);

                    tiles.Add(new EditorTile(
                        tilesets.Value.Id,
                        tile,
                        rect
                    ));
                }
            }

            Children.Clear();

            createTileElements(tiles);
        }

        private void createTileElements(List<EditorTile> tiles)
        {
            foreach(var tile in tiles)
            {
                if(Context!.Scene.TryGetById<Texture>(tile.TilesetId,out var texture))
                {
                    AddChild(
                        new UIButton(
                            new Fixed(Constants.TileSize * 2), 
                            new Fixed(Constants.TileSize * 2)
                        )
                            .SetBorderColor(Color.Transparent)
                            .SetImage(texture!, tile.Source.To<float>())
                    );
                }
            }

            LayoutChildren();
        }
    }
}