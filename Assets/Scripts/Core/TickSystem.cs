using CoinTowerIdle.Events;
using UnityEngine;

namespace CoinTowerIdle.Core
{
    public class TickSystem : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 20)]
        private float tickRate = 5f;

        private float timer;

        private int tickNumber;

        public float TickInterval => 1f / tickRate;

        private void Update()
        {
            timer += Time.deltaTime;

            while (timer >= TickInterval)
            {
                timer -= TickInterval;

                tickNumber++;

                EventBus.Publish(
                    new GameTick(
                        tickNumber,
                        TickInterval));
            }
        }
    }
}