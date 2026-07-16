using TMPro;
using UnityEngine;
using CoinTowerIdle.Events;

namespace CoinTowerIdle.UI
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text moneyText;

        private void OnEnable()
        {
            EventBus.Subscribe<MoneyChangedEvent>(UpdateMoney);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MoneyChangedEvent>(UpdateMoney);
        }

        private void Start()
        {
            UpdateMoney(new MoneyChangedEvent(
                Managers.EconomyManager.Instance.Money));
        }

        private void UpdateMoney(MoneyChangedEvent e)
        {
            moneyText.text = "$" + e.Money.ToString("N0");
        }
    }
}