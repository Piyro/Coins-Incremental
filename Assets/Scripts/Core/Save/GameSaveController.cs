using CoinTowerIdle.Economy;
using CoinTowerIdle.Managers;
using CoinTowerIdle.PassiveIncome;
using UnityEngine;

namespace CoinTowerIdle.SaveSystem
{
    public class GameSaveController : MonoBehaviour
    {
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private PassiveIncomeManager passiveIncomeManager;

        [SerializeField] private float autoSaveInterval = 30f;

        private float timer;

        private void Start()
        {
            LoadGame();
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if (timer >= autoSaveInterval)
            {
                timer = 0f;
                SaveGame();
            }
        }

        public void SaveGame()
        {
            SaveData data = new();

            data.Money = EconomyManager.Instance.Money;
            data.LifetimeMoney = EconomyManager.Instance.LifetimeMoneyEarned;
            data.PrestigeTokens = PrestigeManager.Instance.PrestigeTokens;


            foreach (var pair in upgradeManager.Upgrades)
            {
                data.UpgradeLevels.Add(pair.Value.Level);
            }

            foreach (var business in passiveIncomeManager.Assets)
            {
                data.BusinessLevels.Add(business.Level);
            }

            SaveManager.Instance.Save(data);

            Debug.Log("Game Saved");
        }

        public void LoadGame()
        {
            SaveData data = SaveManager.Instance.Load();
            PrestigeManager.Instance.SetPrestigeTokens(data.PrestigeTokens);

            if (data == null)
            {
                Debug.Log("No save found.");
                return;
            }

            EconomyManager.Instance.SetMoney(data.Money);
            EconomyManager.Instance.SetLifetimeMoney(data.LifetimeMoney);

            upgradeManager.LoadLevels(data.UpgradeLevels);
            passiveIncomeManager.LoadLevels(data.BusinessLevels);

            Debug.Log("Game Loaded");
        }

        public void DeleteSave()
        {
            SaveManager.Instance.DeleteSave();
            Debug.Log("Save Deleted");
        }
    }
}