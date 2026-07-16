namespace CoinTowerIdle.Pooling
{
    /// <summary>
    /// Implemented by any object that is managed by an ObjectPool.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Called every time the object is taken from the pool.
        /// </summary>
        void OnSpawn();

        /// <summary>
        /// Called every time the object is returned to the pool.
        /// </summary>
        void OnDespawn();
    }
}