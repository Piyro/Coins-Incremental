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

        public double LifetimeMoneyEarned { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Money = GameConstants.StartingMoney;
        }

        public void AddMoney(double amount)
        {
            Money += amount;
            LifetimeMoneyEarned += amount;

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
            Debug.Log($"SpendMoney: Money={Money}, Cost={amount}");

            if (Money < amount)
            {
                Debug.Log("Not enough money!");
                return false;
            }

            Money -= amount;

            Debug.Log($"Purchase successful. Remaining: {Money}");

            EventBus.Publish(new MoneyChangedEvent(Money));

            return true;
        }

        public void SetMoney(double money)
        {
            Money = money;

            EventBus.Publish(new MoneyChangedEvent(Money));
        }

        public void SetLifetimeMoney(double money)
        {
            LifetimeMoneyEarned = money;
        }

        public void AddPassiveIncome(double income)
        {
            PassiveIncomePerSecond += income;
        }

        public void ResetProgress()
        {
            SetMoney(0);
            SetLifetimeMoney(0);
        }

    }
}