using System;
using System.Collections.Generic;

namespace CoinTowerIdle.Save
{
    [Serializable]
    public class SaveData
    {
        public int Version = 1;

        public double Money = 25;

        public double PrestigeCurrency;

        public double TotalLifetimeMoney;

        public List<int> PurchasedUpgrades = new();

        public List<int> PurchasedPrestigeNodes = new();

        public List<int> PassiveAssetLevels = new();

        public int TowerStage;

        public long LastSaveTicks;
    }
}