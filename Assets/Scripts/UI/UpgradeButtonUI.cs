using CoinTowerIdle.Economy;
using CoinTowerIdle.Events;
using CoinTowerIdle.Managers;
using CoinTowerIdle.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoinTowerIdle.UI
{
    public class UpgradeButtonUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button button;
        [SerializeField]
        private Image background;

        [SerializeField]
        private Color affordableColor;

        [SerializeField]
        private Color lockedColor;

        [SerializeField]
        private Color maxColor;

        private UpgradeInstance instance;
        private UpgradeManager manager;

        public void Initialize(
            UpgradeManager manager,
            UpgradeInstance instance)
        {
            this.manager = manager;
            this.instance = instance;

            button.onClick.AddListener(Buy);

            Refresh();
        }

        public void Refresh()
        {
            nameText.text =
                instance.Definition.DisplayName;

            levelText.text =
                $"Lv {instance.Level}";

            costText.text =
                "$" +
                NumberFormatter.Format(
                    instance.Cost);

            button.interactable =
                EconomyManager.Instance.Money >=
                instance.Cost;

            if (instance.Level >=
    instance.Definition.MaxLevel)
            {
                button.interactable = false;
                background.color = maxColor;
            }
            else if (EconomyManager.Instance.Money >=
                     instance.Cost)
            {
                button.interactable = true;
                background.color = affordableColor;
            }
            else
            {
                button.interactable = false;
                background.color = lockedColor;
            }
        }

        private void Buy()
        {
            if (manager.Purchase(
                instance.Definition.ID))
            {
                //Refresh();
            }
        }

        private void OnEnable()
        {
            EventBus.Subscribe<MoneyChangedEvent>(OnMoneyChanged);
            EventBus.Subscribe<UpgradePurchasedEvent>(OnUpgradePurchased);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MoneyChangedEvent>(OnMoneyChanged);
            EventBus.Unsubscribe<UpgradePurchasedEvent>(OnUpgradePurchased);
        }

        private void OnMoneyChanged(MoneyChangedEvent e)
        {
            Refresh();
        }

        private void OnUpgradePurchased(
            UpgradePurchasedEvent e)
        {
            if (e.UpgradeID ==
                instance.Definition.ID)
            {
                Refresh();
            }
        }
    }
}