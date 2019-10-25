using UnityEngine;
using System.IO;
using TMPro;

namespace CaromBilliard
{
    public class StatsLoader : MonoBehaviour
    {
        public static void LoadingStats(string fileName, ref PlayerStats stats)
        {
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";

            if (!File.Exists(path))
                return;

            string jsonString = File.ReadAllText(path);
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }

        public static void SavingStats(string fileName, PlayerStats stats)
        {
            string jsonString = JsonUtility.ToJson(stats);

            using (StreamWriter file = File.CreateText(Application.streamingAssetsPath + "/" + fileName + ".json"))
            {
                file.Write(jsonString);
            }
        }
    }
}
