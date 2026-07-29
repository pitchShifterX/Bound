using GameEngine.Utilities;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;

namespace GameEngine.World.Map.Locations
{
    public class LocationService
    {
        private ECSService _ecs;

        public LocationService(ECSService ecs)
        {
            _ecs = ecs;
        }

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
        }
    }
}