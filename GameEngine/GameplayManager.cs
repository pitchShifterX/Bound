using GameEngine.Event.Input;
using GameEngine.Scene;
using GameEngine.SharedInterface;

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

        public GameplayManager(ISceneContext context)
        {
            SceneContext = context;

            GameplayContext = new GameplayContext(SceneContext);
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
    }
}