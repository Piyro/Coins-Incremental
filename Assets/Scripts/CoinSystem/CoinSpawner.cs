using CoinTowerIdle.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinTowerIdle.CoinSystem
{
    public class CoinSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Coin coinPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private CoinFactory coinFactory;
        [SerializeField] private CoinSpawnSettings settings;

        [Header("Coin")]
        [SerializeField]
        private Vector3 baseCoinScale = new(1f, 0.1f, 1f);

        [Header("Spawn")]
        [SerializeField]
        private DropPattern pattern = DropPattern.Single;

        [SerializeField]
        private Vector2 spawnSpread = new(0.08f, 0.02f);

        private float cooldown;

        private void Awake()
        {
            if (coinPrefab == null)
                Debug.LogError("Coin Prefab missing.", this);

            if (spawnPoint == null)
                Debug.LogError("Spawn Point missing.", this);

            if (coinFactory == null)
                Debug.LogError("Coin Factory missing.", this);

            if (settings == null)
                Debug.LogError("Coin Spawn Settings missing.", this);
        }

        private void Update()
        {
            cooldown -= Time.deltaTime;

            // Arcade drop button.
            if (Mouse.current != null &&
                Mouse.current.leftButton.wasPressedThisFrame)
            {
                TrySpawn();
            }
        }

        public void TrySpawn()
        {
            if (cooldown > 0f)
                return;

            if (StatManager.Instance == null)
            {
                Debug.LogError("StatManager is missing.", this);
                return;
            }

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
                    SpawnCoin(Vector3.left * 0.30f);
                    SpawnCoin(Vector3.zero);
                    SpawnCoin(Vector3.right * 0.30f);
                    break;
            }
        }

        private void SpawnCoin(Vector3 offset)
        {
            if (coinFactory == null ||
                coinPrefab == null ||
                spawnPoint == null ||
                settings == null)
            {
                Debug.LogError(
                    "CoinSpawner is missing a required reference.",
                    this);

                return;
            }

            CoinData data = coinFactory.CreateCoin();

            Vector3 randomOffset =
                spawnPoint.right *
                Random.Range(
                    -spawnSpread.x,
                    spawnSpread.x);

            randomOffset +=
                spawnPoint.forward *
                Random.Range(
                    -spawnSpread.y,
                    spawnSpread.y);

            Vector3 position =
                spawnPoint.position +
                offset +
                randomOffset;

            Coin coin = Instantiate(
                coinPrefab,
                position,
                spawnPoint.rotation);

            coin.Initialize(data);

            coin.transform.localScale =
                Vector3.Scale(
                    baseCoinScale,
                    data.Scale);

            // No launch force.
            // Gravity handles the coin naturally.
            coin.Rigidbody.linearVelocity = Vector3.zero;
            coin.Rigidbody.angularVelocity = Vector3.zero;

            if (settings.randomTorque > 0f)
            {
                coin.Rigidbody.AddTorque(
                    Random.insideUnitSphere *
                    settings.randomTorque,
                    ForceMode.Impulse);
            }
        }
    }
}