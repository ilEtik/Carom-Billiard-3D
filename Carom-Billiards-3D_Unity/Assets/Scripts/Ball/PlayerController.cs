using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Class for the player so that he can controll one ball.
    /// </summary>
    public class PlayerController : BallMotor
    {
        public override void ProvideService()
        {
            ServiceLocator.ProvideService(this);
        }

        public float minForce = 1f;
        public float maxForce = 20f;
        public float forceMultiplier = 5f;

        private float curForce;
        private float CurForce
        {
            get { return curForce; }
            set { curForce = Mathf.Clamp(value, minForce, maxForce); }
        }

        public event Action<float, float, float> OnChargeForce;
        public event Action<float> OnApplyForce;

        private void Update()
        {
            GetMouse("Mouse X");
            Charging(KeyCode.Space);
        }

        /// <summary>
        /// Checks the mouse axis value to rotate the ball.
        /// </summary>
        /// <param name="axisName"> The axis on wich the player can rotate. </param>
        void GetMouse(string axisName)
        {
            RotateBall(Input.GetAxisRaw(axisName));
        }

        /// <summary>
        /// Let the player charge the force for shooting the ball.
        /// </summary>
        /// <param name="inputKey"> The Key to charge the force. </param>
        void Charging(KeyCode inputKey)
        {
            if (isMoving)
                return;

            if (Input.GetKey(inputKey))
            {
                CurForce += Time.deltaTime * forceMultiplier;

                if (OnChargeForce != null)
                    OnChargeForce(CurForce, minForce, maxForce);
            }
            else if (Input.GetKeyUp(inputKey))
            {
                invoker.AddCommand(new ShootBallCommand(this, scoreSystem.CurShots, CurForce, transform.position, transform.rotation));
                CurForce = 0;

                if (OnApplyForce != null)
                    OnApplyForce(CurForce);
            }
        }
    }
}