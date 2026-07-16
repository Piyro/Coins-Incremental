using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [System.Serializable]
    public class CoinSpawnSettings
    {
        [Header("Physics")]

        public float launchForce = 1.0f;

        public float randomHorizontalForce = 0.15f;

        public float randomTorque = 10f;

        [Header("Gameplay")]

        public float coinLifetime = 45f;

        public float dropCooldown = 0.8f;

        public double baseCoinValue = 1;

        [Header("Coin Chances")]

        [Range(0, 1)]
        public float criticalChance = 0.05f;

        [Range(0, 1)]
        public float luckyChance = 0.03f;

        [Range(0, 1)]
        public float goldenChance = 0.01f;
    }
}