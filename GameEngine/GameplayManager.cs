using GameEngine.Event.Input;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;
using GameEngine.World.Map.Triggers.Actions;
using GameEngine.World.Map.Triggers.Conditions;

namespace GameEngine
{
    public abstract class GameplayManager : IUpdatable, IRenderable
    {
        /// <summary>
        /// Maintains resources and provides API for interacting with 
        /// core systems.
        /// </summary>
        protected ISceneContext SceneContext { get; init; }

        /// <summary>
        /// The brain behind gameplay. 
        /// </summary>
        protected IGameplayContext? GameplayContext { get; set; }

        /// <summary>
        /// Mods interact with GameRegistries to register pre-defined 
        /// triggers, unit prefabs, etc.
        /// </summary>
        protected GameRegistries Registries { get; init; } = new();

        public GameplayManager(ISceneContext context)
        {
            SceneContext = context;

            RegisterModContent();

            GameplayContext = new GameplayContext(SceneContext, Registries);
        }

        public abstract void Start();

        public virtual void ProcessInput(IRecordInput input)
        {
            GameplayContext?.Process(input);
        }

        public virtual void Update(float? delta)
        {
            GameplayContext?.Update(delta);
        }

        public virtual void Render()
        {
            GameplayContext?.Render();
        }

        /// <summary>
        /// Register mod prefabs and other content that will be used by map files.
        /// </summary>
        protected virtual void RegisterModContent()
        {
            Registries.Triggers.RegisterCondition(
                "Always",
                args => new AlwaysCondition()
            );

            Registries.Triggers.RegisterCondition(
                "PlayerBringsUnitToLocation",
                args => new PlayerBringsUnitToLocationCondition(
                    args.Int(0),
                    args.String(1),
                    args.String(2)
                )
            );

            Registries.Triggers.RegisterCondition(
                "ElapsedTime",
                args => new ElapsedTimeCondition(
                    args.Float(0)
                )
            );

            Registries.Triggers.RegisterAction(
                "CreateUnitAtLocation",
                args => new CreateUnitAtLocationAction(
                    args.String(0),
                    args.Int(1),
                    args.String(2)
                )
            );

            Registries.Triggers.RegisterAction(
                "KillAllUnitsAtLocation",
                args => new KillAllUnitsAtLocationAction(
                    args.String(0),
                    args.Int(1),
                    args.String(2)
                )
            );

            Registries.Triggers.RegisterAction(
                "SetTriggerGroupStatus",
                args => new SetTriggerGroupStatusAction(
                    args.String(0),
                    args.Bool(1)
                )
            );

            Registries.Triggers.RegisterAction(
                "WriteToConsole",
                args => new WriteToConsoleAction(
                    args.String(0)
                )
            );
            
            Registries.Triggers.RegisterAction(
                "Wait",
                args => new WaitAction(
                    args.Float(0)
                )
            );
        }
    }
}