using UnityEngine;

namespace CaromBilliard
{
    public class MainMenuScore : ScoreSystem
    {
        private void Start()
        {
            Invoke("LoadStats", .01f);
        }
    }
}