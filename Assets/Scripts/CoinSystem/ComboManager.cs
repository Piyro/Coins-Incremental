using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    public class ComboManager : MonoBehaviour
    {
        [SerializeField]
        private float comboDuration = 2f;

        [SerializeField]
        private float comboIncrease = 0.10f;

        private float timer;

        public int ComboCount { get; private set; }

        public float ComboMultiplier { get; private set; } = 1f;

        private void Update()
        {
            if (ComboCount == 0)
                return;

            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                ResetCombo();
            }
        }

        public void RegisterCoin()
        {
            ComboCount++;

            timer = comboDuration;

            ComboMultiplier = 1f + ComboCount * comboIncrease;
        }

        public void ResetCombo()
        {
            ComboCount = 0;
            ComboMultiplier = 1f;
        }
    }
}