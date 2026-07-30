namespace GameEngine.World.ECS.Components.Gameplay
{
    public struct UnitComponent
    {
        public string PrefabName;

        public UnitComponent(string prefabName)
        {
            PrefabName = prefabName;
        }
    }
}