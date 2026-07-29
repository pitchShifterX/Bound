using GameEngine.World.Map.Triggers;
using GameEngine.World.Unit;

namespace GameEngine
{
    public class GameRegistries
    {
        /// <summary>
        /// Mods register unit prefabs here. Entities reference prefabs 
        /// by name to get their pre-defined components.
        /// </summary>
        public UnitPrefabRegistry UnitPrefab { get; init; } = new();

        public TriggerRegistry Triggers { get; init; } = new();
    }
}