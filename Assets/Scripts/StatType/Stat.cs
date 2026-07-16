using System.Collections.Generic;

namespace CoinTowerIdle.Stats
{
    public class Stat
    {
        public float BaseValue;

        private readonly List<StatModifier> modifiers = new();

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
        }

        public void ClearModifiers()
        {
            modifiers.Clear();
        }

        public float Value
        {
            get
            {
                float flat = BaseValue;
                float percent = 0f;
                float multiplier = 1f;

                foreach (var mod in modifiers)
                {
                    switch (mod.Type)
                    {
                        case ModifierType.Flat:
                            flat += mod.Value;
                            break;

                        case ModifierType.Percent:
                            percent += mod.Value;
                            break;

                        case ModifierType.Multiplier:
                            multiplier *= mod.Value;
                            break;
                    }
                }

                return flat * (1f + percent) * multiplier;
            }
        }
    }
}