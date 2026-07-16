using System;

namespace CoinTowerIdle.Data
{
    [Serializable]
    public class PlayerStatistics
    {
        public int CoinsDropped;

        public int CoinsCollected;

        public int JackpotCount;

        public double TotalMoneyEarned;

        public double TotalMoneySpent;

        public double PassiveMoneyEarned;

        public double ActiveMoneyEarned;

        public TimeSpan PlayTime;
    }
}