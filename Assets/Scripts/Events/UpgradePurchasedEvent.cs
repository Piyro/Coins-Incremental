namespace CoinTowerIdle.Events
{
    public readonly struct UpgradePurchasedEvent
    {
        public readonly int UpgradeID;

        public UpgradePurchasedEvent(int id)
        {
            UpgradeID = id;
        }
    }
}