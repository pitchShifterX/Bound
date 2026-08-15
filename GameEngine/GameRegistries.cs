using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers;
using GameEngine.World.Map.Triggers.Actions;
using GameEngine.World.Map.Triggers.Conditions;
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

        /// <summary>
        /// Define default trigger conditions and actions
        /// </summary>
        public void Defaults()
        {
            Triggers.RegisterCondition(
                "Always",
                args => new AlwaysCondition()
            );

            Triggers.RegisterCondition(
                "PlayerBringsUnitToLocation",
                args => new PlayerBringsUnitToLocationCondition(
                    args.Int(0),
                    args.String(1),
                    args.String(2)
                )
            );

            Triggers.RegisterCondition(
                "ElapsedTime",
                args => new ElapsedTimeCondition(
                    args.Float(0)
                )
            );

            Triggers.RegisterAction(
                "CreateUnitAtLocation",
                args => new CreateUnitAtLocationAction(
                    args.String(0),
                    args.Int(1),
                    args.String(2)
                )
            );

            Triggers.RegisterAction(
                "KillAllUnitsAtLocation",
                args => new KillAllUnitsAtLocationAction(
                    args.String(0),
                    args.Int(1),
                    args.String(2)
                )
            );

            Triggers.RegisterAction(
                "SetMusic",
                args => new SetMusicAction(
                    args.String(0),
                    args.Bool(1)
                )
            );

            Triggers.RegisterAction(
                "SetTriggerGroupStatus",
                args => new SetTriggerGroupStatusAction(
                    args.String(0),
                    args.Bool(1)
                )
            );

            Triggers.RegisterAction(
                "WriteToConsole",
                args => new WriteToConsoleAction(
                    args.String(0)
                )
            );
            
            Triggers.RegisterAction(
                "Wait",
                args => new WaitAction(
                    args.Float(0)
                )
            );
        }
    }
}