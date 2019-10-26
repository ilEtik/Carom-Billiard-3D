using UnityEngine;

namespace CaromBilliard
{
    public class BallHitCommand : Command
    {
        private Vector3 PrePosition { get; set; }

        public BallHitCommand(Ball ball, Vector3 prePos, int executeInd) : base(ball, executeInd)
        {
            PrePosition = prePos;
        }

        public override void Execute() { }

        public override void Undo()
        {
            Motor.transform.position = PrePosition;
        }
    }
}