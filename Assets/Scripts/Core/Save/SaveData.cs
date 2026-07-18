using System;
using System.Collections.Generic;

namespace CoinTowerIdle.SaveSystem
{
    [Serializable]
    public class SaveData
    {
        public double Money;
        public double LifetimeMoney;
        public int PrestigeTokens;

        public List<int> UpgradeLevels = new();
        public List<int> BusinessLevels = new();
    }
}