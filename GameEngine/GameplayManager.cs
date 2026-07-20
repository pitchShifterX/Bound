using GameEngine.Scene;
using GameEngine.SharedInterface;
using GameEngine.World;
using GameEngine.World.Map;

namespace GameEngine
{
    public abstract class GameplayManager : IWorldController, IRenderable
    {
        private List<IWorldSystem> _systems = [];

        protected ISceneContext Context { get; init; }

        public GameplayManager(ISceneContext context)
        {
            Context = context;
        }

        public virtual void Load()
        {
            foreach(var system in _systems)
            {
                system.Load();
            }
        }

        public virtual void Unload()
        {
            foreach(var system in _systems)
            {
                system.Unload();
            }
        }

        public virtual void Update(float? delta)
        {
            foreach(var system in _systems)
            {
                system.Update(delta);
            }
        }

        public virtual void Render()
        {
            foreach(var system in _systems)
            {
                system.Render();
            }
        }

        public virtual void AddSystem(IWorldSystem system)
            => _systems.Add(system);
    }
}