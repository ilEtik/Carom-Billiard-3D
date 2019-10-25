using UnityEngine;
using TMPro;

namespace CaromBilliard
{
    public class ScoreBoard : MonoBehaviour
    {
        public TextMeshProUGUI nameValue, shotsValue, scoreValue, timeMinValue, timeSecValue;

        private void Start()
        {
            ScoreSystem.Instance.OnStatsChanged += SetScoreBoard;
            ScoreSystem.Instance.OnStatsLoaded += SetScoreBoard;
        }

        public void SetScoreBoard(PlayerStats stats)
        {
            if (nameValue != null)
                nameValue.text = stats.PlayerName;
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