using UnityEngine;
using System.IO;
using TMPro;

namespace CaromBilliard
{
    /// <summary>
    /// Class for saving/loading the stats to/from a json file inside of the StreamingAssets folder
    /// </summary>
    public class StatsLoader : MonoBehaviour
    {
        /// <summary>
        /// Loads the stats from the StreamingAssets folder.
        /// </summary>
        /// <param name="fileName"> The name of the json file. </param>
        /// <param name="stats"> The stats the should be loaded. </param>
        public static void LoadingStats(string fileName, ref PlayerStats stats)
        {
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";

            if (!File.Exists(path))
                return;

            string jsonString = File.ReadAllText(path);
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }

        /// <summary>
        /// Saves the stats to a json file inside of the StreamingAssets folder.
        /// </summary>
        /// <param name="fileName"> The name of the json file. </param>
        /// <param name="stats"> The stats the should be saved. </param>
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
