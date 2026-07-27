namespace CoinTowerIdle.Events
{
    public readonly struct MoneyAddedEvent
    {
        public readonly double Amount;

        public MoneyAddedEvent(double amount)
        {
            Amount = amount;
        }
    }
}