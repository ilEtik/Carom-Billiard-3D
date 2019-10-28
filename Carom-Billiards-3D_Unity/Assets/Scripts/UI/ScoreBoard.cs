using UnityEngine;
using TMPro;

namespace CaromBilliard
{
    public class ScoreBoard : MonoBehaviour, IServiceLocator
    {        
        void IServiceLocator.ProvideService() { }

        private ScoreSystem scoreSystem;

        public void GetService()
        {
            scoreSystem = ServiceLocator.GetService<ScoreSystem>();
        }

        public TextMeshProUGUI shotsValue, scoreValue, timeMinValue, timeSecValue;

        private void Start()
        {
            scoreSystem.OnStatsChanged += SetScoreBoard;
            scoreSystem.OnStatsLoaded += SetScoreBoard;
        }

        public void SetScoreBoard(PlayerStats stats)
        {
            if (shotsValue != null)
                shotsValue.text = stats.Shots.ToString();
            if (scoreValue != null)
                scoreValue.text = stats.Score.ToString();
            if (timeMinValue != null)
                timeMinValue.text = stats.TimeMin.ToString("00.");
            if (timeSecValue != null)
                timeSecValue.text = stats.TimeSec.ToString("00.");
        }
    }
}