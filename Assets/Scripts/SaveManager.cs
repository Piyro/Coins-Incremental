using System.IO;
using UnityEngine;

namespace CoinTowerIdle.SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get; private set; }

        private string SavePath =>
            Path.Combine(Application.persistentDataPath, "save.json");

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);

            Debug.Log($"Saved: {SavePath}");
        }

        public SaveData Load()
        {
            if (!File.Exists(SavePath))
                return null;

            string json = File.ReadAllText(SavePath);

            Debug.Log("Save Loaded");

            return JsonUtility.FromJson<SaveData>(json);
        }

        public void DeleteSave()
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);
        }

    }
}