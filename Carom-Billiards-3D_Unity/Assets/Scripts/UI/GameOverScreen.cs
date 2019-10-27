using UnityEngine;

namespace CaromBilliard
{
    public class GameOverScreen : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService() { }

        private IngameScoreSystem scoreSystem;

        void IServiceLocator.GetService()
        {
            scoreSystem = ServiceLocator.GetService<IngameScoreSystem>();
        }

        public GameObject gameOverScreen;

        private void Start()
        {
            gameOverScreen.SetActive(false);
            scoreSystem.OnGameOver += GameOver;
        }

        void GameOver()
        {
            gameOverScreen.SetActive(true);
            scoreSystem.Invoke("LoadStats", .1f);
        }
    }
}
