using System.Runtime.InteropServices;
using GameEngine.World.ECS.Entities;

namespace GameEngine.World.ECS
{
    public class ECSService
    {
        private int _nextEntityId = 0;
        private readonly Dictionary<int, int> _entities = [];
        private readonly Dictionary<Type, IComponentCollection> _componentCollections = [];

        public WorldEntityHandle CreateEntity()
        {
            var id = _nextEntityId++;
            _entities[id] = 1;

            return new WorldEntityHandle { Id = id, Generation = 1 };
        }

        public bool IsValid(WorldEntityHandle handle)
        {
            return _entities.TryGetValue(handle.Id, out int gen) && 
                gen == handle.Generation;
        }

        public IEnumerable<int> GetEntitiesWith<T>()
            where T : struct
        {
            return getComponentCollection<T>().Storage.Keys;
        }

        public IEnumerable<int> GetEntitiesWith<T1, T2>() 
            where T1 : struct 
            where T2 : struct
        {
            var s1 = GetStorage<T1>();
            var s2 = GetStorage<T2>();

            if (s1.Count > s2.Count)
            {
                foreach (var id in s2.Keys)
                {
                    if (s1.ContainsKey(id)) yield return id;
                }
            }
            else
            {
                foreach (var id in s1.Keys)
                {
                    if (s2.ContainsKey(id)) yield return id;
                }
            }
        }

        public IEnumerable<int> GetEntitiesWith<T1, T2, T3>() 
            where T1 : struct
            where T2 : struct
            where T3 : struct
        {
            var s1 = GetStorage<T1>();
            var s2 = GetStorage<T2>();
            var s3 = GetStorage<T3>();

            if (s1.Count <= s2.Count && s1.Count <= s3.Count)
            {
                foreach (var id in s1.Keys)
                {
                    if (s2.ContainsKey(id) && s3.ContainsKey(id))
                        yield return id;
                }
            }
            else if (s2.Count <= s1.Count && s2.Count <= s3.Count)
            {
                foreach (var id in s2.Keys)
                {
                    if (s1.ContainsKey(id) && s3.ContainsKey(id))
                        yield return id;
                }
            }
            else
            {
                foreach (var id in s3.Keys)
                {
                    if (s1.ContainsKey(id) && s2.ContainsKey(id))
                        yield return id;
                }
            }
        }

        public Dictionary<int, T> GetStorage<T>() where T : struct
        {
            return getComponentCollection<T>().Storage;
        }

        public WorldEntityHandle? GetEntityHandle(int entityId)
        {
            if(_entities.TryGetValue(entityId, out var gen))
            {
                return new WorldEntityHandle { Id = entityId, Generation = gen };
            }

            return null;
        }

        public void DestroyEntity(WorldEntityHandle handle)
        {
            if(!IsValid(handle)) return;

            foreach(var collection in _componentCollections.Values)
            {
                collection.Remove(handle.Id);
            }

            _entities[handle.Id]++;
        }

        public void AddComponent<T>(int entityId, T component) where T : struct
        {
            getComponentCollection<T>().Storage[entityId] = component;
        }

        public bool HasComponent<T>(int entityId) where T : struct
        {
            if(!_componentCollections.TryGetValue(typeof(T), out var collection))
                return false;

            return ((ComponentCollection<T>)collection).Storage.ContainsKey(entityId);
        }

        public void RemoveComponent<T>(int entityId) where T : struct
        {
            getComponentCollection<T>().Storage.Remove(entityId);
        }

        public ref T GetComponent<T>(int entityId) where T : struct
        {
            var storage = getComponentCollection<T>().Storage;

            if(!storage.ContainsKey(entityId))
            {
                throw new System.Exception(
                    $"Entity {entityId} does not have component {typeof(T).Name}"
                );
            }

            return ref CollectionsMarshal.GetValueRefOrNullRef(storage, entityId);
        }

        private interface IComponentCollection
        {
            void Remove(int entityId);
        }

        private class ComponentCollection<T> : IComponentCollection where T : struct
        {
            public readonly Dictionary<int, T> Storage = [];
            public void Remove(int entityId) => Storage.Remove(entityId);
        }

        private ComponentCollection<T> getComponentCollection<T>() where T : struct
        {
            var type = typeof(T);

            if(!_componentCollections.TryGetValue(type, out var collection))
            {
                collection = new ComponentCollection<T>();
                _componentCollections[type] = collection;
            }

            return (ComponentCollection<T>)collection;
        }
    }
}