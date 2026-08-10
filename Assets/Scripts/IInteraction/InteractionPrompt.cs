using UnityEngine;

namespace CoinTowerIdle.Interaction
{
    public class InteractionPrompt : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void Show()
        {
            panel.SetActive(true);
        }

        public void Hide()
        {
            panel.SetActive(false);
        }
    }
}