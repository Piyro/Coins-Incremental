using TMPro;
using UnityEngine;
using CoinTowerIdle.Events;

namespace CoinTowerIdle.UI
{
    public class IncomeUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        private void OnEnable()
        {
            EventBus.Subscribe<PassiveIncomeChangedEvent>(
                IncomeChanged);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<PassiveIncomeChangedEvent>(
                IncomeChanged);
        }

        private void Start()
        {
            text.text = "$0/sec";
        }

        private void IncomeChanged(
            PassiveIncomeChangedEvent e)
        {
            text.text =
                $"${e.IncomePerSecond:0.##}/sec";
        }
    }
}