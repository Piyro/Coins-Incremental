using TMPro;
using UnityEngine;
using CoinTowerIdle.Managers;

namespace CoinTowerIdle.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text moneyText;

        private void Update()
        {
            if (EconomyManager.Instance == null)
                return;

            moneyText.text = $"${EconomyManager.Instance.Money:N0}";
        }
    }
}