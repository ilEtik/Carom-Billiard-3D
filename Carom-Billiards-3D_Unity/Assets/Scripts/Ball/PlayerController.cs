using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class PlayerController : BallMotor
    {
        public float minForce = 1f;
        public float maxForce = 20f;
        public float forceMultiplier = 5f;

        private float curForce;
        private float CurForce
        {
            get { return curForce; }
            set
            {
                if (value < minForce)
                    curForce = minForce;
                else if (value > maxForce)
                    curForce = maxForce;
                else
                    curForce = value;
            }
        }

        public static event Action<float, float, float> OnChargeForce;
        public static event Action<float> OnApplyForce;

        private void Update()
        {
            GetMouse("Mouse X");
            Charging(KeyCode.Space);
        }

        void GetMouse(string axisName)
        {
            RotateCam(Input.GetAxisRaw(axisName));
        }

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
                invoker.ExecuteCommand(new ShootBallCommand(this, CurForce, transform.position, transform.rotation));
                CurForce = 0;

                if (OnApplyForce != null)
                    OnApplyForce(CurForce);
            }
        }
    }
}