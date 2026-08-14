using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Sounds;
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

        /// <summary>
        /// Mods register custom trigger conditions and actions here. 
        /// Pre-defined conditions and actions are accessible to mods 
        /// by default.
        /// </summary>
        public TriggerRegistry Triggers { get; init; } = new();

        /// <summary>
        /// Mods register tilesets that can be referenced by map files.
        /// </summary>
        public TilesetRegistry Tilesets { get; init; } = new();

        /// <summary>
        /// Mods register music and sounds to be referenced by map files.
        /// </summary>
        public SoundRegistry Sounds { get; init; } = new();
    }
}