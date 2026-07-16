using System;
using System.Collections.Generic;
using UnityEditor;

namespace CoinTowerIdle.Data
{
    [Serializable]
    public class PlayerProgress
    {
        // ---------- Economy ----------

        public double Money = 25;

        public double LifetimeMoney = 25;

        public double PrestigeCurrency = 0;

        // ---------- Progress ----------

        public int PrestigeCount;

        public int TowerLevel;

        // ---------- Upgrades ----------

        public Dictionary<int, int> UpgradeLevels =
            new();

        // ---------- Businesses ----------

        public Dictionary<int, int> BusinessLevels =
            new();

        // ---------- Prestige ----------

        public HashSet<int> PrestigeNodes =
            new();

        // ---------- Statistics ----------

        public PlayerStatistics Statistics =
            new();

        // ---------- Settings ----------

        public PlayerSettings Settings =
            new();
    }
}