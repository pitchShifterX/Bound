using GameEngine.Utilities;
using GameEngine.World.ECS.Components.Graphics;
using GameEngine.World.ECS.Components.Spatial;
using GameEngine.World.Map;

namespace GameEngine.World.ECS.Systems
{
    public class MapInitializationSystem
    {
        private ECSService _ecs;
        private MapData _map;

        public MapInitializationSystem(ECSService service, MapData map)
        {
            _ecs = service;
            _map = map;
        }

        public void InitializeMapEntities()
        {
            initializeLocations();
        }

        private void initializeLocations()
        {
            if(_map?.Metadata?.Locations == null) return;

            foreach(var location in _map.Metadata.Locations)
            {
                if(location?.Tiles == null) continue;

                var entityHandle = _ecs.CreateEntity();

                float worldX = location.Tiles.X * Constants.TileSize;
                float worldY = location.Tiles.Y * Constants.TileSize;
                float worldWidth = location.Tiles.Width * Constants.TileSize;
                float worldHeight = location.Tiles.Height * Constants.TileSize;

                _ecs.AddComponent(entityHandle.Id, new Rectangle2DComponent
                {
                    Value = new Rectangle<float>
                    {
                        X = worldX,
                        Y = worldY,
                        Width = worldWidth,
                        Height = worldHeight
                    }
                });

                _ecs.AddComponent(entityHandle.Id, new BorderRenderComponent
                {
                    BorderColor = location.Color
                });
            }
        }
    }
}