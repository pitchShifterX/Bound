using GameEngine.World.ECS;

namespace GameEngine.World.Unit
{
    /// <summary>
    /// <para>Defines a unit template.</para>
    /// <para>
    /// In CreatePrefab() you will attach components to define your unit.
    /// </para>
    /// </summary>
    public interface IUnitPrefab
    {
        public string Name { get; }
        public void CreatePrefab(int entityId, ECSService service);
    }
}