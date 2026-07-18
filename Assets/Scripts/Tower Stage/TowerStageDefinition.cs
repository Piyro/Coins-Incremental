using UnityEngine;

namespace CoinTowerIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Coin Tower/Tower Stage")]
    public class TowerStageDefinition : ScriptableObject
    {
        public string DisplayName;

        public double RequiredMoney;

        public GameObject Prefab;
    }
}