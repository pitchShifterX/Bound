using GameEngine.Utilities;
using GameEngine.World.Player;

namespace GameEngine.World.Map
{
    public class MapMetadata
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public List<PlayerData>? Players { get; set; }

        public Vector2<int>? GetSize()
        {
            if(Width == null || Height == null) return null;
            
            return new(Width.Value, Height.Value);
        }
    }
}