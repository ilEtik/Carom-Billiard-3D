using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Command that shoots the ball of the player.
    /// </summary>
    public class ShootBallCommand : Command
    {
        private Vector3 PrePosition { get; set; }
        private Quaternion PreRotation { get; set; }
        private float Force { get; set; }

        public ShootBallCommand(BallMotor motor, int executeInd, float force, Vector3 prePos, Quaternion preRot) : base (motor, executeInd)
        {
            Force = force;
            PrePosition = prePos;
            PreRotation = preRot;
        }

        public override void Execute()
        {
            PrePosition = Motor.transform.position;
            Motor.ShootBall(Force);
        }

        public override void Undo()
        {
            Motor.transform.position = PrePosition;
            Motor.transform.rotation = PreRotation;
        }
    }
}