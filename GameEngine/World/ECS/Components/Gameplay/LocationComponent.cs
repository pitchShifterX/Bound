namespace GameEngine.World.ECS.Components.Gameplay
{
    public struct LocationComponent
    {
        public string Id;

        public LocationComponent(string locationId)
        {
            Id = locationId;
        }
    }
}