using System;

namespace CoinTowerIdle.Data
{
    [Serializable]
    public class PlayerSettings
    {
        public bool MusicEnabled = true;

        public bool SoundEnabled = true;

        public float MusicVolume = 1;

        public float SfxVolume = 1;

        public bool AutoDropEnabled;

        public bool ScreenShake = true;
    }
}