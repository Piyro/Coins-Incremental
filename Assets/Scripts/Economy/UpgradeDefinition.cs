using UnityEngine;
using CoinTowerIdle.Stats;

namespace CoinTowerIdle.ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "Upgrade",
        menuName = "Coin Tower/Upgrade")]
    public class UpgradeDefinition : BaseDefinition
    {
        [Header("Economy")]
        public double BaseCost = 25;
        public float CostGrowth = 1.18f;
        public int MaxLevel = 100;

        [Header("Stat")]

        public StatType TargetStat;

        public ModifierType ModifierType;

        public float ValuePerLevel = 1f;
    }
}