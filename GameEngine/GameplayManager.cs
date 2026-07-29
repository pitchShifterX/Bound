using GameEngine.Event.Input;
using GameEngine.Resources;
using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World.ECS;
using GameEngine.World.ECS.Systems;
using GameEngine.World.Input;
using GameEngine.World.Map;
using GameEngine.World.Map.Tiles;

namespace GameEngine
{
    public abstract class GameplayManager : ILoadable, IUpdatable, IRenderable
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

        public virtual void Load()
        {
            GameplayContext?.Load();
        }

        public virtual void Unload()
        {
            GameplayContext?.Unload();
        }

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
        protected abstract void RegisterModContent();
    }
}