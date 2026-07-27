using CoinTowerIdle.Economy;
using CoinTowerIdle.PassiveIncome;
using CoinTowerIdle.SaveSystem;
using CoinTowerIdle.Tower;
using UnityEngine;

namespace CoinTowerIdle.Managers
{
    public class PrestigeController : MonoBehaviour
    {
        public static PrestigeController Instance { get; private set; }

        [Header("References")]
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private PassiveIncomeManager passiveIncomeManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private GameSaveController saveController;

        [Header("Prestige")]
        [SerializeField] private double prestigeRequirement = 1_000_000;

        public int PrestigeTokens { get; private set; }

        public float PrestigeMultiplier => 1f + PrestigeTokens * 0.1f;

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

            if (lifetime < prestigeRequirement)
                return 0;

            return Mathf.FloorToInt(
                Mathf.Sqrt((float)(lifetime / prestigeRequirement)));
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

        public void Prestige()
        {
            int reward = CalculatePrestigeReward();

            if (reward <= 0)
                return;

            AddPrestigeTokens(reward);

            EconomyManager.Instance.ResetProgress();

            upgradeManager.ResetProgress();

            passiveIncomeManager.ResetProgress();

            towerManager.ResetProgress();

            saveController?.SaveGame();

            Debug.Log($"Prestiged! +{reward} Tokens");
        }
    }
}