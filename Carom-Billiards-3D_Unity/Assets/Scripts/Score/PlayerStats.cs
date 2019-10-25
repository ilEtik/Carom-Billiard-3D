using UnityEngine;
using System;

namespace CaromBilliard
{
    [Serializable]
    public class PlayerStats
    {
        public PlayerStats() { }
        public PlayerStats(string name, int score, int shots, float timeMin, float timeSec)
        {
            PlayerName = name;
            Score = score;
            Shots = shots;
            TimeMin = timeMin;
            TimeSec = timeSec;
        }

        public string PlayerName = "new Player";
        public int Score = 0;
        public int Shots = 0;
        public float TimeMin = 0;
        public float TimeSec = 0;
    }
}