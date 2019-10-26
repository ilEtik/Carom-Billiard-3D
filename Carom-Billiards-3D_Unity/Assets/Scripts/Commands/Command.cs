using UnityEngine;

namespace CaromBilliard
{
    public abstract class Command
    {
        public Ball Motor { get; private set; }
        public int ExecutedRoundInd { get; private set; }

        public Command (Ball motor, int executeInd)
        {
            Motor = motor;
            ExecutedRoundInd = executeInd;
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}
