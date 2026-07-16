using System.Collections.Generic;
using UnityEngine;

namespace CoinTowerIdle.Stats
{
    public class StatManager : MonoBehaviour
    {
        public static StatManager Instance { get; private set; }

        private readonly Dictionary<StatType, Stat> stats =
            new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            RegisterDefaults();
        }

        private void RegisterDefaults()
        {
            RegisterStat(StatType.CoinValue, 1f);

            RegisterStat(StatType.DropCooldown, 1f);

            RegisterStat(StatType.MovementSpeed, 8f);

            RegisterStat(StatType.CriticalChance, 0.05f);

            RegisterStat(StatType.PassiveIncome, 1f);

            RegisterStat(StatType.PusherSpeed, 1f);

            RegisterStat(StatType.CoinWeight, 1f);

            RegisterStat(StatType.CoinSize, 1f);

            RegisterStat(StatType.GoldenChance, 0.01f);

            RegisterStat(StatType.LuckyChance, 0.03f);
        }

        private void RegisterStat(
    StatType type,
    float value)
        {
            if (stats.ContainsKey(type))
            {
                Debug.LogWarning($"{type} already registered.");
                return;
            }

            stats.Add(type, new Stat(value));
        }

        public float GetValue(StatType type)
        {
            if (stats.TryGetValue(type, out Stat stat))
                return stat.Value;

            Debug.LogError($"Stat '{type}' has not been registered.");

            return 0f;
        }

        public void AddModifier(
            StatType type,
            StatModifier modifier)
        {
            stats[type].AddModifier(modifier);
        }

        public void ResetAll()
        {
            foreach (var stat in stats.Values)
            {
                stat.ClearModifiers();
            }
        }
    }
}