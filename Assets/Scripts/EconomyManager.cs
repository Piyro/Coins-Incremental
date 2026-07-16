using UnityEngine;
using CoinTowerIdle.Core;
using CoinTowerIdle.Events;

namespace CoinTowerIdle.Managers
{
    public class EconomyManager : Singleton<EconomyManager>
    {
        public double Money { get; private set; }

        private bool dirty;

        public double PassiveIncomePerSecond { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Money = GameConstants.StartingMoney;
        }

        public void AddMoney(double amount)
        {
            Money += amount;

            Debug.Log($"Money: {Money}");

            dirty = true;
        }
        private void OnEnable()
        {
            EventBus.Subscribe<GameTick>(OnGameTick);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<GameTick>(OnGameTick);
        }

        private void OnGameTick(GameTick tick)
        {
            if (!dirty)
                return;

            dirty = false;

            EventBus.Publish(
                new MoneyChangedEvent(Money));
        }

        public bool SpendMoney(double amount)
        {
            if (Money < amount)
                return false;

            Money -= amount;

            dirty = true;

            return true;
        }   

        public void AddPassiveIncome(double income)
        {
            PassiveIncomePerSecond += income;
        }

    }
}