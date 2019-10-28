using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Motor for the ball that the Player can move.
    /// </summary>
    public class BallMotor : Ball
    {
        private BallsManager ballsManager;

        public override void GetService()
        {
            base.GetService();
            ballsManager = ServiceLocator.GetService<BallsManager>();
        }

        private Vector3 ballRot = Vector3.zero;
        private Vector3 direction = Vector3.zero;
        public bool isMoving;

        public event Action<Vector3[]> OnAiming;

        internal override void InitializeStart()
        {
            base.InitializeStart();
            ballsManager.OnMoving += (a) => isMoving = a;
        }

        public override void ShootBall(float force)
        {
            if (BallRb != null)
                BallRb.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        /// <summary>
        /// Sets the value of how much the ball should be rotated
        /// </summary>
        /// <param name="rotationValueY"> How much the ball should be rotated. </param>
        public void RotateBall(float rotationValueY)
        {
            ballRot.y = rotationValueY;
        }

        private void FixedUpdate()
        {
            PerformRotation();
        }

        /// <summary>
        /// Rotate the ball in the direction where the ball should be shooted.
        /// </summary>
        private void PerformRotation()
        {
            if (!isMoving && OnAiming != null)
                OnAiming(points());

            transform.Rotate(ballRot, Space.Self);
        }

        /// <summary>
        /// The positions for displaying the shooting direction
        /// </summary>
        /// <returns> Vector3 array that stores the shooting direction positions. </returns>
        private Vector3[] points()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5, 1, QueryTriggerInteraction.Collide))
                return new Vector3[2] { transform.position, hit.point };

            return new Vector3[2] { transform.position, transform.forward * 5 + transform.position };
        }
    }
}