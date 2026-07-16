using CoinTowerIdle.Managers;
using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [RequireComponent(typeof(Collider))]
    public class RewardZone : MonoBehaviour
    {
        [SerializeField]
        private RewardType rewardType;

        [SerializeField]
        private ComboManager comboManager;

        [Header("Bonus Chances")]
        [SerializeField]
        private float bonusCoinChance = 0.10f;

        [SerializeField]
        private float jackpotChance = 0.01f;

        private void OnTriggerEnter(Collider other)
        {
            Coin coin = other.GetComponent<Coin>();

            if (coin == null)
                return;

            comboManager.RegisterCoin();

            double reward = RewardCalculator.Calculate(
                coin,
                comboManager.ComboMultiplier);

            switch (rewardType)
            {
                case RewardType.Bonus:

                    if (Random.value < bonusCoinChance)
                    {
                        reward *= 2;
                    }

                    break;

                case RewardType.Jackpot:

                    if (Random.value < jackpotChance)
                    {
                        reward *= 50;
                    }

                    break;
            }

            EconomyManager.Instance.AddMoney(reward);

            coin.ReturnToPool();
        }
    }
}