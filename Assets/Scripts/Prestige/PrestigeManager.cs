using UnityEngine;

namespace CoinTowerIdle.Managers
{
    public class PrestigeManager : MonoBehaviour
    {
        public static PrestigeManager Instance { get; private set; }

        public int PrestigeTokens { get; private set; }

        public float PrestigeMultiplier => 1f + PrestigeTokens * 0.1f;

        private const double PrestigeRequirement = 1_000_000d;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public int CalculatePrestigeReward()
        {
            double lifetime = EconomyManager.Instance.LifetimeMoneyEarned;

            if (lifetime < PrestigeRequirement)
                return 0;

            return Mathf.FloorToInt(
                Mathf.Sqrt((float)(lifetime / PrestigeRequirement)));
        }

        public bool CanPrestige()
        {
            return CalculatePrestigeReward() > 0;
        }

        public void SetPrestigeTokens(int amount)
        {
            PrestigeTokens = amount;
        }

        public void AddPrestigeTokens(int amount)
        {
            PrestigeTokens += amount;
        }
    }
}