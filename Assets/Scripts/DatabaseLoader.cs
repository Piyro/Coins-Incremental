using UnityEngine;
using CoinTowerIdle.ScriptableObjects;

namespace CoinTowerIdle.Core
{
    public static class DatabaseLoader
    {
        private static GameDatabase database;

        public static GameDatabase Database
        {
            get
            {
                if (database == null)
                    database = Resources.Load<GameDatabase>("GameDatabase");

                return database;
            }
        }
    }
}