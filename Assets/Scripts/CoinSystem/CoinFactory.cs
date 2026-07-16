using CoinTowerIdle.Stats;
using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public class CoinFactory : MonoBehaviour
    {
        [SerializeField]
        private CoinSpawnSettings settings;

        public CoinData CreateCoin()
        {
            CoinRoll roll = RollCoin();

            return new CoinData
            {
                Value = roll.Value,
                Type = roll.Type,
                Mass = roll.Mass,
                Scale = Vector3.one * roll.Size,
                Lifetime = roll.Lifetime
            };
        }

        private CoinRoll RollCoin()
        {
            CoinType type = RollCoinType();

            double value =
                StatManager.Instance.GetValue(
                    StatType.CoinValue);

            switch (type)
            {
                case CoinType.Critical:
                    value *= 2;
                    break;

                case CoinType.Lucky:
                    value *= 3;
                    break;

                case CoinType.Golden:
                    value *= 5;
                    break;

                case CoinType.Jackpot:
                    value *= 20;
                    break;
            }

            return new CoinRoll
            {
                Type = type,
                Value = value,
                Mass = StatManager.Instance.GetValue(
                    StatType.CoinWeight),

                Size = StatManager.Instance.GetValue(
                    StatType.CoinSize),

                Lifetime = settings.coinLifetime
            };
        }

        private CoinType RollCoinType()
        {
            float r = Random.value;

            float golden =
                StatManager.Instance.GetValue(
                    StatType.GoldenChance);

            if (r < golden)
                return CoinType.Golden;

            r -= golden;

            float lucky =
                StatManager.Instance.GetValue(
                    StatType.LuckyChance);

            if (r < lucky)
                return CoinType.Lucky;

            r -= lucky;

            float critical =
                StatManager.Instance.GetValue(
                    StatType.CriticalChance);

            if (r < critical)
                return CoinType.Critical;

            return CoinType.Normal;
        }
    }
}