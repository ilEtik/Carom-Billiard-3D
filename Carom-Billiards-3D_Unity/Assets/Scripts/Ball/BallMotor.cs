using UnityEngine;
using System;

namespace CaromBilliard
{
    public class BallMotor : Ball
    {
        private BallsManager ballsManager;

        public override void GetService()
        {
            base.GetService();
            ballsManager = ServiceLocator.GetService<BallsManager>();
        }

        public GameObject Cam { get; private set; }

        private Vector3 camRot = Vector3.zero;
        private Vector3 direction = Vector3.zero;
        public bool isMoving;

        public event Action<Vector3[]> OnAiming;

        internal override void InitializeStart()
        {
            base.InitializeStart();
            Cam = GameObject.FindGameObjectWithTag("MainCamera");
            ballsManager.OnMoving += (a) => isMoving = a;
        }

        public override void ShootBall(float force)
        {
            if (BallRb != null)
                BallRb.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        public void RotateCam(float rotationValueY)
        {
            camRot.y = rotationValueY;
        }

        private void FixedUpdate()
        {
            PerformRotation();
        }

        private void PerformRotation()
        {
            if (Cam == null)
            {
                Debug.LogError("Camera is null");
                return;
            }

            if (!isMoving && OnAiming != null)
                OnAiming(points());

            transform.Rotate(camRot, Space.Self);
        }

        private Vector3[] points()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5, 1, QueryTriggerInteraction.Collide))
                return new Vector3[2] { transform.position, hit.point };

            return new Vector3[2] { transform.position, transform.forward * 5 + transform.position };
        }
    }
}