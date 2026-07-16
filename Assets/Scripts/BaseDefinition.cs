using UnityEngine;

namespace CoinTowerIdle.ScriptableObjects
{
    public abstract class BaseDefinition : ScriptableObject
    {
        public int ID;

        public string DisplayName;

        [TextArea]
        public string Description;

        public Sprite Icon;
    }
}