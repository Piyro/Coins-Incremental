using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [System.Serializable]
    public class PusherSettings
    {
        [Header("Motion")]

        public float strokeLength = 1.2f;

        public float cycleTime = 2.5f;

        public AnimationCurve movementCurve =
            AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Gameplay")]

        public float speedMultiplier = 1f;
    }
}