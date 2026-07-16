using UnityEngine;

namespace CoinTowerIdle.Pooling
{
    /// <summary>
    /// Base class for pooled MonoBehaviours.
    /// </summary>
    public abstract class PooledObject : MonoBehaviour, IPoolable
    {
        public virtual void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnDespawn()
        {
            gameObject.SetActive(false);
        }
    }
}