using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CoinTowerIdle.PassiveIncome;
using CoinTowerIdle.Managers;
using CoinTowerIdle.Events;

namespace CoinTowerIdle.UI
{
    public class BusinessButtonUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text incomeText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button buyButton;

        private PassiveIncomeManager manager;
        private PassiveAssetInstance asset;

        private float refreshTimer;

        private void Update()
        {
            refreshTimer += Time.deltaTime;

            if (refreshTimer >= 0.25f)
            {
                refreshTimer = 0f;
                Refresh();
            }
        }

        public void Initialize(PassiveIncomeManager manager, PassiveAssetInstance asset)
        {
            this.manager = manager;
            this.asset = asset;

            buyButton.onClick.AddListener(Buy);

            Refresh();
        }

        private void OnEnable()
        {
            EventBus.Subscribe<MoneyChangedEvent>(OnMoneyChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MoneyChangedEvent>(OnMoneyChanged);

            buyButton.onClick.RemoveListener(Buy);
        }

        private void OnMoneyChanged(MoneyChangedEvent e)
        {
            Refresh();
        }

        private void Buy()
        {
            if (manager.Purchase(asset))
            {
                Refresh();
            }
        }

        private void Refresh()
        {

            nameText.text = asset.Definition.DisplayName;
            levelText.text = $"Level {asset.Level}";
            incomeText.text = $"${asset.Income:0.##}/sec";
            costText.text = $"${asset.Cost:0}";

            buyButton.interactable =
                EconomyManager.Instance.Money >= asset.Cost;
        }


    }
}