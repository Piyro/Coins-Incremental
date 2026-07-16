using System;
using CoinTowerIdle.Pooling;
using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public class CoinPool : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField]
        private Coin prefab;

        [SerializeField]
        private int initialPoolSize = 300;

        private ObjectPool<Coin> pool;

        public static CoinPool Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            // FIX: Replaced 'transform' with 'null'.
            // This prevents the coins from becoming children of this manager.
            // They will now spawn cleanly in the root of the scene hierarchy,
            // preserving their exact prefab component settings and collider shapes.
            pool = new ObjectPool<Coin>(
                prefab,
                initialPoolSize,
                null);
        }

        public Coin Spawn(CoinData data,
                          Vector3 position,
                          Vector3 impulse)
        {
            Coin coin = pool.Get();

            coin.Initialize(data);

            coin.transform.position = position;

            // Re-enforces the exact local scale from your original prefab file
            if (prefab != null)
            {
                coin.transform.localScale = prefab.transform.localScale;
            }

            coin.ReturnRequested -= ReturnCoin;
            coin.ReturnRequested += ReturnCoin;

            coin.Launch(position, impulse);

            return coin;
        }

        private void ReturnCoin(Coin coin)
        {
            coin.ReturnRequested -= ReturnCoin;

            pool.Release(coin);
        }

        public PoolStatistics GetStatistics()
        {
            return new PoolStatistics(
                pool.ActiveCount,
                pool.AvailableCount,
                pool.TotalCount);
        }
    }
}