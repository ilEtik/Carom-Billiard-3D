using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// The Stats that the player reaches.
    /// </summary>
    [Serializable]
    public class PlayerStats
    {
        public PlayerStats() { }
        public PlayerStats(int score, int shots, float timeMin, float timeSec)
        {
            Score = score;
            Shots = shots;
            TimeMin = timeMin;
            TimeSec = timeSec;
        }

        public int Score = 0;
        public int Shots = 0;
        public float TimeMin = 0;
        public float TimeSec = 0;
    }
}