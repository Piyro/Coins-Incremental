using CoinTowerIdle.Economy;
using CoinTowerIdle.PassiveIncome;
using CoinTowerIdle.SaveSystem;
using CoinTowerIdle.Tower;
using UnityEngine;

namespace CoinTowerIdle.Managers
{
    public class PrestigeController : MonoBehaviour
    {
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private PassiveIncomeManager passiveIncomeManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private GameSaveController saveController;

        public void Prestige()
        {
            int reward =
                PrestigeManager.Instance.CalculatePrestigeReward();

            if (reward <= 0)
                return;

            PrestigeManager.Instance.AddPrestigeTokens(reward);

            EconomyManager.Instance.ResetProgress();

            upgradeManager.ResetProgress();

            passiveIncomeManager.ResetProgress();

            Debug.Log($"Prestiged! +{reward} Tokens");

            towerManager.ResetProgress();

            FindFirstObjectByType<GameSaveController>()?.SaveGame();

            saveController.SaveGame();

        }
    }
}