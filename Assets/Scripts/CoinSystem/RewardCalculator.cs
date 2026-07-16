using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public static class RewardCalculator
    {
        public static double Calculate(Coin coin, float comboMultiplier)
        {
            double reward = coin.Value;

            reward *= comboMultiplier;

            switch (coin.Type)
            {
                case CoinType.Critical:
                    reward *= 2;
                    break;

                case CoinType.Golden:
                    reward *= 5;
                    break;

                case CoinType.Jackpot:
                    reward *= 20;
                    break;
            }

            return reward;
        }
    }
}