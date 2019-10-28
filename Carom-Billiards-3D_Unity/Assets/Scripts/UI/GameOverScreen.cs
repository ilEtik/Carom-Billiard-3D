using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Controlls the game over screen.
    /// </summary>
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

        /// <summary>
        /// called when the game is over.
        /// </summary>
        void GameOver()
        {
            gameOverScreen.SetActive(true);
            scoreSystem.Invoke("LoadStats", .1f);
        }
    }
}
