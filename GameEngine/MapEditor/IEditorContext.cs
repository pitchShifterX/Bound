using GameEngine.MapEditor.Tools;
using GameEngine.World;
using GameEngine.World.Map;

namespace GameEngine.MapEditor
{
    public interface IEditorContext : IWorldContext
    {
        public IMapContext? Map { get; }
        public PlacementTool? PlacementTool { get; set; }
    }
}