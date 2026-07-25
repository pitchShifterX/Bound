using GameEngine.Graphics.Primitives;
using GameEngine.Utilities;

namespace GameEngine.World.Map.Locations
{
    public class Location
    {
        public required string Name { get; set; }
        public required Rectangle<int> Tiles { get; set; }
        public required Color Color { get; set; }
    }
}