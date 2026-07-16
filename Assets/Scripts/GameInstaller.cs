using UnityEngine;

namespace CoinTowerIdle.Managers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private EconomyManager economyManager;

        private void Awake()
        {
            if (gameManager == null)
                Debug.LogError("GameManager Missing");

            if (economyManager == null)
                Debug.LogError("EconomyManager Missing");
        }
    }
}