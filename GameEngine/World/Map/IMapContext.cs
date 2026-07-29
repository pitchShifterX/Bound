namespace GameEngine.World.Map
{
    public interface IMapContext : IMapView
    {
        public void LoadMap(string path);
    }
}