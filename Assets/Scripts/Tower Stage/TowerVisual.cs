using System.Collections;
using UnityEngine;

namespace CoinTowerIdle.Tower
{
    public class TowerVisual : MonoBehaviour
    {
        [SerializeField]
        private float growDuration = 0.5f;

        public void PlaySpawnAnimation()
        {
            StopAllCoroutines();
            StartCoroutine(GrowRoutine());
        }

        private IEnumerator GrowRoutine()
        {
            Vector3 target = transform.localScale;

            transform.localScale = Vector3.zero;

            float t = 0;

            while (t < growDuration)
            {
                t += Time.deltaTime;

                float p = t / growDuration;

                p = Mathf.SmoothStep(0, 1, p);

                transform.localScale =
                    Vector3.LerpUnclamped(
                        Vector3.zero,
                        target,
                        p);

                yield return null;
            }

            transform.localScale = target;
        }
    }
}