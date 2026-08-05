using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Spatial;

namespace GameEngine
{
    public class GameplaySystems : IUpdatable
    {
        private ECSService _ecs;
        private SpatialHashGrid _spatialGrid;
        private TerrainService _terrain;

        private SpatialSystem _spatialSystem;
        private MovementSystem _movementSystem;
        private UnitOrderSystem _unitOrderSystem;

        public GameplaySystems(ECSService ecs, SpatialHashGrid grid, TerrainService terrain)
        {
            _ecs = ecs;
            _spatialGrid = grid;
            _terrain = terrain;

            _spatialSystem = new SpatialSystem(_ecs, _spatialGrid);
            _movementSystem = new MovementSystem(_ecs, _spatialGrid, _terrain);
            _unitOrderSystem = new UnitOrderSystem(_ecs);
        }

        public void Update(float? delta)
        {
            _spatialSystem.Update(delta);
            _unitOrderSystem.Update(delta);
            _movementSystem.Update(delta);
        }
    }
}