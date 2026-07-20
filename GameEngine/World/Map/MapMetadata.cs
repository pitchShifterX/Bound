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
    }
}