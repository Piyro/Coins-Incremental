using CoinTowerIdle.Events;
using CoinTowerIdle.Managers;
using CoinTowerIdle.ScriptableObjects;
using CoinTowerIdle.Stats;
using System.Collections.Generic;
using UnityEngine;

namespace CoinTowerIdle.Economy
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField]
        private GameDatabase database;

        private readonly Dictionary<int, UpgradeInstance> upgrades =
            new();

        private void Awake()
        {
            if (database == null)
            {
                Debug.LogError("GameDatabase not assigned!");
                return;
            }

            if (database.Upgrades == null)
            {
                Debug.LogError("GameDatabase.Upgrades is NULL!");
                return;
            }

            foreach (var definition in database.Upgrades)
            {
                if (definition == null)
                {
                    Debug.LogWarning("Null UpgradeDefinition found.");
                    continue;
                }

                upgrades.Add(
                    definition.ID,
                    new UpgradeInstance
                    {
                        Definition = definition
                    });
            }
        }

        public bool Purchase(int id)
        {
            if (!upgrades.TryGetValue(id, out UpgradeInstance upgrade))
            {
                Debug.LogError($"Upgrade {id} not found.");
                return false;
            }

            if (EconomyManager.Instance == null)
            {
                Debug.LogError("EconomyManager missing.");
                return false;
            }

            if (StatManager.Instance == null)
            {
                Debug.LogError("StatManager missing.");
                return false;
            }

            if (!EconomyManager.Instance.SpendMoney(upgrade.Cost))
                return false;

            upgrade.Level++;

            StatManager.Instance.AddModifier(
                upgrade.Definition.TargetStat,
                new StatModifier(
                    upgrade.Definition.ModifierType,
                    upgrade.Definition.ValuePerLevel));

            EventBus.Publish(
                new UpgradePurchasedEvent(
                    upgrade.Definition.ID));

            return true;
        }

        public IReadOnlyDictionary<int, UpgradeInstance>
            Upgrades => upgrades;
    }
}