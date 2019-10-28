using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Command that is called when a ball was hit.
    /// </summary>
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