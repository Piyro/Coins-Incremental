namespace CoinTowerIdle.Events
{
    public readonly struct PassiveIncomeChangedEvent
    {
        public readonly double IncomePerSecond;

        public PassiveIncomeChangedEvent(double income)
        {
            IncomePerSecond = income;
        }
    }
}