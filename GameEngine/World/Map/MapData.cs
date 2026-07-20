namespace GameEngine.World.Map
{
    public class MapData
    {
        public MapMetadata? Metadata { get; set; }

        public override string ToString()
        {
            var meta = Metadata;

            return $"{meta?.Author} made {meta?.Title} with {meta?.Players?.Count} players.";
        }
    }
}