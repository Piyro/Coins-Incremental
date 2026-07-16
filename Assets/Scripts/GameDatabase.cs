using System.Collections.Generic;
using UnityEngine;

namespace CoinTowerIdle.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Coin Tower/Game Database")]
    public class GameDatabase : ScriptableObject
    {
        public List<UpgradeDefinition> Upgrades = new();

        public List<PassiveAssetDefinition> PassiveAssets = new();

        public List<PrestigeNodeDefinition> PrestigeNodes = new();
    }
}