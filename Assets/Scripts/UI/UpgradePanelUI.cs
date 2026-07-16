using UnityEngine;
using CoinTowerIdle.Economy;

namespace CoinTowerIdle.UI
{
    public class UpgradePanelUI : MonoBehaviour
    {
        [SerializeField]
        UpgradeManager manager;

        [SerializeField]
        UpgradeUI prefab;

        [SerializeField]
        Transform content;

        void Start()
        {
            foreach (var pair in manager.Upgrades)
            {
                UpgradeUI ui =
                    Instantiate(prefab, content);

                ui.Initialize(
                    manager,
                    pair.Value);
            }
        }
    }
}