using UnityEngine;
using System.Collections.Generic;

namespace CoinTowerIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Coin Tower/Prestige Node")]
    public class PrestigeNodeDefinition : BaseDefinition
    {
        public int Cost;

        public List<PrestigeNodeDefinition> Children;

        public bool Repeatable;

        public float BonusValue;
    }
}