using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Loads the stats inside of the main menu.
    /// </summary>
    public class MainMenuScore : ScoreSystem
    {
        private void Start()
        {
            Invoke("LoadStats", .01f);
        }
    }
}