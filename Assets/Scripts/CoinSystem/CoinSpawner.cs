using CoinTowerIdle.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinTowerIdle.CoinSystem
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField]
        private Coin coinPrefab;

        [SerializeField]
        private Vector3 coinScale = new(1f, 0.1f, 1f);

        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private CoinSpawnSettings settings;

        [SerializeField]
        private CoinFactory coinFactory;

        [SerializeField]
        private DropPattern pattern = DropPattern.Single;

        private float cooldown;

        private void Update()
        {
            cooldown -= Time.deltaTime;

            // Temporary debug input.
            if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
            {
                TrySpawn();
            }
        }

        public void TrySpawn()
        {
            if (cooldown > 0f)
                return;

            cooldown = StatManager.Instance.GetValue(
                StatType.DropCooldown);

            switch (pattern)
            {
                case DropPattern.Single:
                    SpawnCoin(Vector3.zero);
                    break;

                case DropPattern.Double:
                    SpawnCoin(Vector3.left * 0.25f);
                    SpawnCoin(Vector3.right * 0.25f);
                    break;

                case DropPattern.Triple:
                    SpawnCoin(Vector3.left * 0.3f);
                    SpawnCoin(Vector3.zero);
                    SpawnCoin(Vector3.right * 0.3f);
                    break;
            }
        }



        private void SpawnCoin(Vector3 offset)
        {
            if (coinFactory == null)
            {
                Debug.LogError("CoinFactory is NULL");
                return;
            }


            if (spawnPoint == null)
            {
                Debug.LogError("SpawnPoint is NULL");
                return;
            }

            if (settings == null)
            {
                Debug.LogError("CoinSpawnSettings is NULL");
                return;
            }

            CoinData data = coinFactory.CreateCoin();

            Vector3 impulse =
                Vector3.down * settings.launchForce +
                Random.insideUnitSphere * settings.randomHorizontalForce;

            Coin coin = Instantiate(
                coinPrefab,
                spawnPoint.position + offset,
                Quaternion.identity);

            //coin.transform.localScale = coinScale;

            coin.Initialize(data);

            coin.Launch(
                spawnPoint.position + offset,
                impulse);

            coin.Rigidbody.AddTorque(
                Random.insideUnitSphere * settings.randomTorque,
                ForceMode.Impulse);
        }

        private CoinType RollCoinType()
        {
            float r = Random.value;

            if (r < settings.goldenChance)
                return CoinType.Golden;

            r -= settings.goldenChance;

            if (r < settings.luckyChance)
                return CoinType.Lucky;

            r -= settings.luckyChance;

            if (r < settings.criticalChance)
                return CoinType.Critical;

            return CoinType.Normal;
        }
    }
}