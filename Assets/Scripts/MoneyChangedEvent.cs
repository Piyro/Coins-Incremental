namespace CoinTowerIdle.Events
{
    public struct MoneyChangedEvent
    {
        public double Money;

        public MoneyChangedEvent(double money)
        {
            Money = money;
        }
    }
}