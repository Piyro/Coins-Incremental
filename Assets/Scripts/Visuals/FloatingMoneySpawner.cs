using CoinTowerIdle.Events;
using UnityEngine;

namespace CoinTowerIdle.UI
{
    public class FloatingMoneySpawner : MonoBehaviour
    {
        [SerializeField] private FloatingMoneyText prefab;
        [SerializeField] private RectTransform parent;

        [Header("Spawn Position")]
        [SerializeField] private Vector2 spawnPosition = new(960f, 745f);

        [Header("Random Offset")]
        [SerializeField] private float randomRadius = 30f;

        private void OnEnable()
        {
            EventBus.Subscribe<MoneyAddedEvent>(OnMoneyAdded);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MoneyAddedEvent>(OnMoneyAdded);
        }

        private void OnMoneyAdded(MoneyAddedEvent e)
        {
            FloatingMoneyText text = Instantiate(prefab, parent);

            RectTransform rect = text.GetComponent<RectTransform>();

            rect.anchoredPosition =
                spawnPosition + Random.insideUnitCircle * randomRadius;

            text.Show(e.Amount);
        }
    }
}