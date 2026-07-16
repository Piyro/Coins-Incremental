using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CoinTowerIdle.Economy;
using CoinTowerIdle.Events;
using CoinTowerIdle.Managers;

namespace CoinTowerIdle.UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text descriptionText;
        [SerializeField] TMP_Text levelText;
        [SerializeField] TMP_Text costText;

        [SerializeField] Button buyButton;

        UpgradeManager manager;
        UpgradeInstance instance;

        public void Initialize(
            UpgradeManager manager,
            UpgradeInstance instance)
        {
            this.manager = manager;
            this.instance = instance;

            buyButton.onClick.AddListener(Buy);

            Refresh();
        }

        void OnEnable()
        {
            EventBus.Subscribe<MoneyChangedEvent>(MoneyChanged);
        }

        void OnDisable()
        {
            EventBus.Unsubscribe<MoneyChangedEvent>(MoneyChanged);
        }

        void MoneyChanged(MoneyChangedEvent e)
        {
            Refresh();
        }

        void Buy()
        {
            manager.Purchase(instance.Definition.ID);
        }

        public void Refresh()
        {
            nameText.text = instance.Definition.DisplayName;

            descriptionText.text =
                instance.Definition.Description;

            levelText.text =
                "Lv " + instance.Level;

            costText.text =
                "$" + instance.Cost.ToString("N0");

            buyButton.interactable =
                EconomyManager.Instance.Money >=
                instance.Cost;
        }
    }
}