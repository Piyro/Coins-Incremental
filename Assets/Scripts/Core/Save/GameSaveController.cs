using System;
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

        [Header("Offline Progress")]
        [SerializeField] private float maxOfflineHours = 8f;

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
            data.PrestigeTokens = PrestigeController.Instance.PrestigeTokens;
            data.LastSaveTime = DateTime.UtcNow.ToString("O");

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

            if (data == null)
            {
                Debug.Log("No save found.");
                return;
            }

            EconomyManager.Instance.SetMoney(data.Money);
            EconomyManager.Instance.SetLifetimeMoney(data.LifetimeMoney);

            PrestigeController.Instance.SetPrestigeTokens(data.PrestigeTokens);

            upgradeManager.LoadLevels(data.UpgradeLevels);
            passiveIncomeManager.LoadLevels(data.BusinessLevels);

            CalculateOfflineEarnings(data);

            Debug.Log("Game Loaded");
        }

        private void CalculateOfflineEarnings(SaveData data)
        {
            if (string.IsNullOrEmpty(data.LastSaveTime))
                return;

            DateTime lastTime = DateTime.Parse(data.LastSaveTime);

            TimeSpan away = DateTime.UtcNow - lastTime;

            double seconds = away.TotalSeconds;

            if (seconds < 60)
                return;

            seconds = Math.Min(
                seconds,
                maxOfflineHours * 60d * 60d);

            double reward =
                passiveIncomeManager.IncomePerSecond *
                PrestigeController.Instance.PrestigeMultiplier *
                seconds;

            if (reward <= 0)
                return;

            EconomyManager.Instance.AddMoney(reward);

            Debug.Log(
                $"Offline Earnings: +{reward:N0} from {away.TotalMinutes:N0} minutes away.");
        }

        public void DeleteSave()
        {
            SaveManager.Instance.DeleteSave();

            Debug.Log("Save Deleted");
        }
    }
}