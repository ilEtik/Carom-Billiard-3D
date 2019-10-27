using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class BallsManager : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService()
        {
            ServiceLocator.ProvideService(this);
        }

        void IServiceLocator.GetService() { }

        private void Awake()
        {
            Balls = FindObjectsOfType<Ball>();
        }

        public Ball[] Balls { get; private set; } = new Ball[3];

        public Ball[] movingBalls { get; private set; } = new Ball[3];

        public event Action<bool> OnMoving;

        private void FixedUpdate()
        {
            CheckBallsMoving();
        }

        void CheckBallsMoving()
        {
            if (OnMoving == null)
                return;

            for (int i = 0; i < Balls.Length; i++)
            {
                if (Balls[i].IsMoving)
                    movingBalls[i] = Balls[i];
                else
                    movingBalls[i] = null;
            }

            if (movingBalls[0] == null && movingBalls[1] == null && movingBalls[2] == null)
                OnMoving(false);
            else
                OnMoving(true);
        }
    }
}
