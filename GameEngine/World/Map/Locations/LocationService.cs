using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;

namespace GameEngine.World.Map.Locations
{
    public class LocationService
    {
        /// <summary>
        /// Service needed to manage locations, as they are entities.
        /// </summary>
        private ECSService _ecs;

        /// <summary>
        /// Map the location name to the entity id.
        /// </summary>
        private Dictionary<string, int> _locationEntities = [];

        /// <summary>
        /// Map the entity id to the location object.
        /// </summary>
        private Dictionary<int, Location> _entityLocations = [];

        /// <summary>
        /// Service for creating and retrieving locations. Locations 
        /// are created during map initialization.
        /// </summary>
        /// <param name="ecs"></param>
        public LocationService(ECSService ecs)
        {
            _ecs = ecs;
        }

        /// <summary>
        /// Create a location entity for systems to consume their 
        /// components. Locations are created during map initialization, 
        /// and are cached in this service to be retrieved later. Since 
        /// locations will never be destroyed during gameplay, they 
        /// will keep their entity id.
        /// </summary>
        /// <param name="location"></param>
        public void Create(Location location)
        {
            if(location == null) return;

            var locationEntity = _ecs.CreateEntity();

            float worldX = location.Tiles.X * Constants.TileSize;
            float worldY = location.Tiles.Y * Constants.TileSize;
            float worldWidth = location.Tiles.Width * Constants.TileSize;
            float worldHeight = location.Tiles.Height * Constants.TileSize;

            _ecs.AddComponent(locationEntity.Id, new Rectangle2DComponent
            {
                Value = new Rectangle<float>
                {
                    X = worldX,
                    Y = worldY,
                    Width = worldWidth,
                    Height = worldHeight
                }
            });

            _ecs.AddComponent(locationEntity.Id, new BorderRenderComponent
            {
                BorderColor = location.Color
            });

            _locationEntities.Add(location.Name, locationEntity.Id);
            _entityLocations.Add(locationEntity.Id, location);

            Log.Info($"Location {location.Name} created at [{worldX}, {worldY}, {worldWidth}, {worldHeight}]");
        }

        public Location? GetLocationByEntityId(int id)
        {
            return _entityLocations[id];
        }

        public Location? GetLocationByName(string name)
        {
            if(_locationEntities.TryGetValue(name, out var entityId))
            {
                return GetLocationByEntityId(entityId);
            }

            return null;
        }

        public Rectangle<float>? GetWorldBoundsByLocationName(string name)
        {
            if(!_locationEntities.TryGetValue(name, out var entityId))
                return null;
            
            return _ecs.GetComponent<Rectangle2DComponent>(entityId).Value;
        }
    }
}