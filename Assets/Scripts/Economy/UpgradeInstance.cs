using CoinTowerIdle.ScriptableObjects;

namespace CoinTowerIdle.Economy
{
    public class UpgradeInstance
    {
        public UpgradeDefinition Definition;

        public int Level;

        public double Cost =>
            Definition.BaseCost *
            System.Math.Pow(
                Definition.CostGrowth,
                Level);
    }
}