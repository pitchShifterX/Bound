namespace GameEngine.World.Unit
{
    public class UnitPrefabRegistry
    {
        private readonly Dictionary<string, IUnitPrefab> _prefabs = [];

        public IReadOnlyDictionary<string, IUnitPrefab> Prefabs => _prefabs;

        public void Register(IUnitPrefab prefab)
        {
            _prefabs[prefab.Name] = prefab;
        }

        public IUnitPrefab? Get(string prefabName)
        {
            _prefabs.TryGetValue(prefabName, out var prefab);

            return prefab;
        }
    }
}