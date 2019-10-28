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

        private ReplaySystem replaySystem;

        public override void GetService()
        {
            base.GetService();
            replaySystem = ServiceLocator.GetService<ReplaySystem>();
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

        internal override void InitializeStart()
        {
            base.InitializeStart();
            invoker.OnHasCommands += (hasCommands) => canReplay = hasCommands;
        }

        private void Update()
        {
            GetMouse("Mouse X");
            Charging(KeyCode.Space);
            StartReplay(KeyCode.F);
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
            if (Input.GetKeyUp(inputKey))
            {
                invoker.AddCommand(new ShootBallCommand(this, CurForce, transform.position, transform.rotation));
                CurForce = 0;

                if (OnApplyForce != null)
                    OnApplyForce(CurForce);
            }
        }

        private bool canReplay;

        /// <summary>
        /// Starts a replay when the play presses a key.
        /// </summary>
        /// <param name="inputKey"> The key to start the replay. </param>
        void StartReplay(KeyCode inputKey)
        {
            if (canReplay && !isMoving && Input.GetKeyDown(inputKey))
                replaySystem.StartReplay();
        }
    }
}