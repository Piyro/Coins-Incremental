using System;

namespace CoinTowerIdle.Data
{
    [Serializable]
    public class SaveGame
    {
        public int Version = 1;

        public DateTime SaveTime;

        public PlayerProgress Progress =
            new();
    }
}