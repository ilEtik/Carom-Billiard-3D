using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Base class for all score systems.
    /// </summary>
    public class ScoreSystem : MonoBehaviour, IServiceLocator
    {
        public virtual void ProvideService()
        {
            ServiceLocator.ProvideService(this);
        }

        public virtual void GetService() { }

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

        /// <summary>
        /// Saves the stats to The json file.
        /// </summary>
        public void SaveStats()
        {
            StatsLoader.SavingStats("Stats", Stats);
        }

        /// <summary>
        /// Loads the stats from the json file.
        /// </summary>
        public void LoadStats()
        {
            StatsLoader.LoadingStats("Stats", ref Stats);

            if (OnStatsLoaded != null)
                OnStatsLoaded(Stats);
        }
    }
}