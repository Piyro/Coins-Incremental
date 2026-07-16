using UnityEngine;

namespace CoinTowerIdle.CoinSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class CoinPusher : MonoBehaviour
    {
        [SerializeField]
        private PusherSettings settings;

        private Rigidbody rb;

        private Vector3 startPosition;

        private float timer;

        public float SpeedMultiplier
        {
            get => settings.speedMultiplier;
            set => settings.speedMultiplier = Mathf.Max(0.1f, value);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();

            startPosition = transform.position;

            rb.isKinematic = true;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;

            float cycle =
                (timer / settings.cycleTime)
                * settings.speedMultiplier;

            float t = Mathf.PingPong(cycle, 1f);

            float eased = settings.movementCurve.Evaluate(t);

            Vector3 target =
                startPosition +
                transform.forward *
                (eased * settings.strokeLength);

            rb.MovePosition(target);
        }

        public void AddSpeedMultiplier(float multiplier)
        {
            SpeedMultiplier *= multiplier;
        }

        public void ResetSpeed()
        {
            SpeedMultiplier = 1f;
        }
    }
}