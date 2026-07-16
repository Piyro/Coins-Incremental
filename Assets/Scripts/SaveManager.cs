using System.IO;
using UnityEngine;
using CoinTowerIdle.Core;

namespace CoinTowerIdle.Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        private const string FileName = "save.json";

        public SaveData Data { get; private set; }

        private string SavePath =>
            Path.Combine(Application.persistentDataPath, FileName);

        protected override void Awake()
        {
            base.Awake();

            Load();
        }

        public void Save()
        {
            Data.LastSaveTicks = System.DateTime.UtcNow.Ticks;

            string json = JsonUtility.ToJson(Data, true);

            File.WriteAllText(SavePath, json);
        }

        public void Load()
        {
            if (!File.Exists(SavePath))
            {
                Data = new SaveData();
                Save();
                return;
            }

            string json = File.ReadAllText(SavePath);

            Data = JsonUtility.FromJson<SaveData>(json);

            if (Data == null)
                Data = new SaveData();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
                Save();
        }
    }
}