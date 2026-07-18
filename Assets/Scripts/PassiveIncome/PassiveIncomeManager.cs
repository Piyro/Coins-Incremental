using System.Collections.Generic;
using UnityEngine;
using CoinTowerIdle.ScriptableObjects;
using CoinTowerIdle.Managers;
using CoinTowerIdle.Events;

namespace CoinTowerIdle.PassiveIncome
{
    public class PassiveIncomeManager : MonoBehaviour
    {
        [SerializeField]
        private GameDatabase database;

        private readonly List<PassiveAssetInstance> assets = new();

        private double totalIncomePerSecond;

        public IReadOnlyList<PassiveAssetInstance> Assets => assets;

        public double TotalIncomePerSecond => totalIncomePerSecond;

        private void Awake()
        {
            foreach (var definition in database.PassiveAssets)
            {
                assets.Add(new PassiveAssetInstance
                {
                    Definition = definition
                });
            }

            RecalculateIncome();
        }

        private void Update()
        {
            if (totalIncomePerSecond <= 0)
                return;

            EconomyManager.Instance.AddMoney(
                totalIncomePerSecond * Time.deltaTime);
        }

        public bool Purchase(PassiveAssetInstance asset)
        {
            if (!EconomyManager.Instance.SpendMoney(asset.Cost))
                return false;

            asset.Level++;

            RecalculateIncome();

            return true;
        }

        private void RecalculateIncome()
        {
            totalIncomePerSecond = 0;

            foreach (var asset in assets)
            {
                totalIncomePerSecond += asset.Income;
            }

            EventBus.Publish(
                new PassiveIncomeChangedEvent(totalIncomePerSecond));
        }

        public void LoadLevels(List<int> levels)
        {
            for (int i = 0; i < assets.Count && i < levels.Count; i++)
            {
                assets[i].Level = levels[i];
            }

            RecalculateIncome();
        }

        public void ResetLevels()
        {
            foreach (var asset in assets)
            {
                asset.Level = 0;
            }

            RecalculateIncome();
        }

        public void ResetProgress()
        {
            foreach (var asset in assets)
            {
                asset.Level = 0;
            }

            RecalculateIncome();
        }
    }
}