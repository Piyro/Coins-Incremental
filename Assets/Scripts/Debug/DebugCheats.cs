using CoinTowerIdle.CoinSystem;
using CoinTowerIdle.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinTowerIdle.Debugging
{
    public class DebugCheats : MonoBehaviour
    {
        [Header("Cheat Values")]
        [SerializeField] private double moneyAmount = 1000;
        [SerializeField] private int coinsToSpawn = 10;

        [Header("References")]
        [SerializeField] private CoinSpawner coinSpawner;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (keyboard.f1Key.wasPressedThisFrame)
            {
                EconomyManager.Instance.AddMoney(moneyAmount);
                Debug.Log($"Added ${moneyAmount}");
            }

            if (keyboard.f2Key.wasPressedThisFrame)
            {
                for (int i = 0; i < coinsToSpawn; i++)
                {
                    coinSpawner.TrySpawn();
                }
            }

            if (keyboard.f3Key.wasPressedThisFrame)
            {
                EconomyManager.Instance.AddMoney(1_000_000);
                Debug.Log("Added $1,000,000");
            }
        }
    }
}