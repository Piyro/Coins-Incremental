using CoinTowerIdle.ScriptableObjects;

namespace CoinTowerIdle.PassiveIncome
{
    public class PassiveAssetInstance
    {
        public PassiveAssetDefinition Definition;

        public int Level;

        public double Cost =>
            Definition.BaseCost *
            System.Math.Pow(
                Definition.CostGrowth,
                Level);

        public double Income
        {
            get
            {
                if (Level == 0)
                    return 0;

                return Definition.BaseIncome *
                       System.Math.Pow(
                           Definition.IncomeGrowth,
                           Level - 1);
            }
        }
    }
}