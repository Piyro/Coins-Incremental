using UnityEngine;

namespace CoinTowerIdle.Save
{
    public class Autosave : MonoBehaviour
    {
        [SerializeField]
        private float interval = 30f;

        float timer;

        void Update()
        {
            timer += Time.unscaledDeltaTime;

            if (timer < interval)
                return;

            timer = 0;

            SaveManager.Instance.Save();
        }
    }
}