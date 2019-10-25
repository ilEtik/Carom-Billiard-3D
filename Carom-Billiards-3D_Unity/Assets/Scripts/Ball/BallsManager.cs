using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class BallsManager : MonoBehaviour
    {
        public static BallsManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

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
            if(OnMoving == null)
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
