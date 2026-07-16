using UnityEngine;

namespace CoinTowerIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Coin Tower/Passive Asset")]
    public class PassiveAssetDefinition : BaseDefinition
    {
        [Header("Economy")]

        public double BaseCost = 50;

        public float CostGrowth = 1.15f;

        public double BaseIncome = 1;

        public float IncomeGrowth = 1.12f;

        [Header("Visual")]

        public GameObject WorldPrefab;
    }
}