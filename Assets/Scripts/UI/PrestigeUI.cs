using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CoinTowerIdle.Managers;
using CoinTowerIdle.Economy;

namespace CoinTowerIdle.UI
{
    public class PrestigeUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text lifetimeMoneyText;
        [SerializeField] private TMP_Text tokensText;
        [SerializeField] private TMP_Text multiplierText;
        [SerializeField] private TMP_Text rewardText;

        [SerializeField] private Button prestigeButton;

        private void Update()
        {
            var prestige = PrestigeController.Instance;

            if (prestige == null)
                return;

            lifetimeMoneyText.text =
                $"Lifetime: ₺{EconomyManager.Instance.LifetimeMoneyEarned:N0}";

            tokensText.text =
                $"Tokens: {prestige.PrestigeTokens}";

            multiplierText.text =
                $"Multiplier: x{prestige.PrestigeMultiplier:0.0}";

            int reward = prestige.CalculatePrestigeReward();

            rewardText.text =
                $"Reward: +{reward}";

            prestigeButton.interactable = prestige.CanPrestige();
        }
    }
}