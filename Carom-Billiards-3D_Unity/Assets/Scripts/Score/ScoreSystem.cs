using UnityEngine;
using System;

namespace CaromBilliard
{
    public class ScoreSystem : MonoBehaviour
    {
        public static ScoreSystem Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public int scoreToWin = 3;
        public PlayerStats Stats;

        public event Action<PlayerStats> OnStatsChanged;
        public event Action<PlayerStats> OnStatsLoaded;
        public event Action OnGameOver;

        public int CurShots
        {
            get { return Stats.Shots; }
            set
            {
                Stats.Shots = value;

                if (OnStatsChanged != null)
                    OnStatsChanged(Stats);
            }
        }

        public int CurScore
        {
            get { return Stats.Score; }
            set
            {
                Stats.Score = value;

                if (OnStatsChanged != null)
                    OnStatsChanged(Stats);

                if (Stats.Score >= scoreToWin && OnGameOver != null)
                    OnGameOver();
            }
        }

        public float CurTimer
        {
            get { return Stats.TimeSec; }
            set
            {
                if (value > 60)
                {
                    Stats.TimeSec = 0;
                    Stats.TimeMin++;
                }
                else
                    Stats.TimeSec = value;

                if (OnStatsChanged != null)
                    OnStatsChanged(Stats);
            }
        }

        public void SaveStats()
        {
            StatsLoader.SavingStats("Stats", Stats);
        }

        public void LoadStats()
        {
            StatsLoader.LoadingStats("Stats", ref Stats);
            
            if (OnStatsLoaded != null)
                OnStatsLoaded(Stats);
        }
    }
}