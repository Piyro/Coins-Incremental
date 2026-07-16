using System.Collections.Generic;
using UnityEngine;

namespace CoinTowerIdle.Pooling
{
    public class ObjectPool<T> where T : PooledObject
    {
        private readonly Queue<T> available = new();

        private readonly HashSet<T> active = new();

        private readonly T prefab;

        private readonly Transform parent;

        public int ActiveCount => active.Count;

        public int AvailableCount => available.Count;

        public int TotalCount => active.Count + available.Count;

        public ObjectPool(T prefab, int initialSize, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            Prewarm(initialSize);
        }

        public void Prewarm(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CreateInstance();
            }
        }

        private void CreateInstance()
        {
            T obj = Object.Instantiate(prefab, parent);

            obj.OnDespawn();

            available.Enqueue(obj);
        }

        public T Get()
        {
            if (available.Count == 0)
            {
                Prewarm(20);
            }

            T obj = available.Dequeue();

            active.Add(obj);

            obj.OnSpawn();

            return obj;
        }

        public void Release(T obj)
        {
            if (!active.Remove(obj))
                return;

            obj.OnDespawn();

            available.Enqueue(obj);
        }
    }
}