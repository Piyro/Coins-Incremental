namespace CoinTowerIdle.Pooling
{
    public readonly struct PoolStatistics
    {
        public readonly int Active;

        public readonly int Available;

        public readonly int Total;

        public PoolStatistics(int active, int available, int total)
        {
            Active = active;
            Available = available;
            Total = total;
        }
    }
}