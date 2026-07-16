using System.Collections.Generic;
using UnityEngine;
using CoinTowerIdle.ScriptableObjects;
using CoinTowerIdle.Managers;

namespace CoinTowerIdle.PassiveIncome
{
    public class PassiveIncomeManager : MonoBehaviour
    {
        [SerializeField]
        private GameDatabase database;

        private readonly List<PassiveAssetInstance> assets =
            new();

        public IReadOnlyList<PassiveAssetInstance> Assets => assets;

        private void Awake()
        {
            foreach (var asset in database.PassiveAssets)
            {
                assets.Add(
                    new PassiveAssetInstance
                    {
                        Definition = asset
                    });
            }
        }

        private void Update()
        {
            double income = 0;

            foreach (var asset in assets)
            {
                income += asset.Income;
            }

            EconomyManager.Instance.AddMoney(
                income * Time.deltaTime);
        }

        public bool Purchase(PassiveAssetInstance asset)
        {
            if (!EconomyManager.Instance.SpendMoney(asset.Cost))
                return false;

            asset.Level++;

            return true;
        }

        public double TotalIncomePerSecond()
        {
            double total = 0;

            foreach (var asset in assets)
                total += asset.Income;

            return total;
        }
    }
}