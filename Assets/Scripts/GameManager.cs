using UnityEngine;
using CoinTowerIdle.Core;

namespace CoinTowerIdle.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsPaused { get; private set; }

        public void PauseGame()
        {
            IsPaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            IsPaused = false;
            Time.timeScale = 1;
        }

        protected override void Awake()
        {
            base.Awake();

            Application.targetFrameRate = 144;
        }
    }
}