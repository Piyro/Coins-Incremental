using CoinTowerIdle.Utilities;
using TMPro;
using UnityEngine;

namespace CoinTowerIdle.UI
{
    public class FloatingMoneyText : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        [SerializeField] private float duration = 1.2f;

        [SerializeField] private float moveSpeed = 80f;

        [SerializeField] private float scaleAmount = 1.3f;

        private CanvasGroup canvasGroup;

        private RectTransform rect;

        private float timer;

        private Vector3 startScale;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                canvasGroup = gameObject.AddComponent<CanvasGroup>();

            startScale = rect.localScale;
        }

        public void Show(double amount)
        {
            label.text = "+" + NumberFormatter.Format(amount);

            timer = duration;

            canvasGroup.alpha = 1f;

            rect.localScale = startScale;
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            float t = 1f - timer / duration;

            rect.anchoredPosition +=
                Vector2.up * moveSpeed * Time.deltaTime;

            rect.localScale =
                Vector3.Lerp(
                    startScale,
                    startScale * scaleAmount,
                    t);

            canvasGroup.alpha = 1f - t;

            if (timer <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}