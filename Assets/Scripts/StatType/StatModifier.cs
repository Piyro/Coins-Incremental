namespace CoinTowerIdle.Stats
{
    public struct StatModifier
    {
        public ModifierType Type;
        public float Value;

        public StatModifier(
            ModifierType type,
            float value)
        {
            Type = type;
            Value = value;
        }
    }
}