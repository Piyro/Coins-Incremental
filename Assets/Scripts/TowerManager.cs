using UnityEngine;
using CoinTowerIdle.ScriptableObjects;
using CoinTowerIdle.Managers;

namespace CoinTowerIdle.Tower
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private GameDatabase database;
        [SerializeField] private Transform towerRoot;

        private GameObject currentTower;
        private int currentStage = -1;

        private void Update()
        {
            double money = EconomyManager.Instance.LifetimeMoneyEarned;

            int stage = GetStage(money);

            if (stage == currentStage)
                return;

            ChangeStage(stage);
        }

        private int GetStage(double money)
        {
            int result = 0;

            for (int i = 0; i < database.TowerStages.Count; i++)
            {
                if (money >= database.TowerStages[i].RequiredMoney)
                    result = i;
            }

            return result;
        }

        private void ChangeStage(int stage)
        {
            currentStage = stage;

            if (currentTower != null)
                Destroy(currentTower);

            currentTower = Instantiate(
                database.TowerStages[stage].Prefab,
                towerRoot);

            TowerVisual visual =
                currentTower.GetComponent<TowerVisual>();

            if (visual != null)
                visual.PlaySpawnAnimation();
        }

        public void ResetProgress()
        {
            BuildStage(0);
        }
    }
}