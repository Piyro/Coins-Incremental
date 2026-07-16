using UnityEngine;
using CoinTowerIdle.PassiveIncome;

namespace CoinTowerIdle.UI
{
    public class BusinessPanelUI : MonoBehaviour
    {
        [SerializeField]
        private PassiveIncomeManager manager;

        [SerializeField]
        private BusinessButtonUI prefab;

        [SerializeField]
        private Transform content;

        private void Start()
        {
            foreach (var asset in manager.Assets)
            {
                BusinessButtonUI button =
                    Instantiate(prefab, content);

                button.Initialize(
                    manager,
                    asset);
            }
        }
    }
}