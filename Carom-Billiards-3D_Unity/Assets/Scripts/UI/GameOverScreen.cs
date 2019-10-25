using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaromBilliard
{
    public class GameOverScreen : MonoBehaviour
    {
        public GameObject gameOverScreen;

        private void Start() 
        {
            IngameScoreSystem.Instance.OnGameOver += GameOver;
        }

        void GameOver()
        {
            gameOverScreen.SetActive(true);
            ScoreSystem.Instance.Invoke("LoadStats", .1f);
        }
    }
}
