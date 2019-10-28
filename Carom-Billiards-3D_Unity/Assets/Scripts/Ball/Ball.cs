using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Base class for all balls.
    /// </summary>
    public class Ball : MonoBehaviour, IServiceLocator
    {
        public virtual void ProvideService() { }

        internal CommandInvoker invoker;
        internal IngameScoreSystem scoreSystem;
        private PlayerController playerController;

        public virtual void GetService()
        {
            playerController = ServiceLocator.GetService<PlayerController>();
            invoker = ServiceLocator.GetService<CommandInvoker>();
            scoreSystem = ServiceLocator.GetService<IngameScoreSystem>();
        }

        [HideInInspector]
        public Rigidbody BallRb;

        public bool IsMoving { get { return BallRb.velocity.magnitude > .1f; } }

        public event Action<GameObject, GameObject> OnHit;

        public int lastShot;

        private void Start()
        {
            InitializeStart();
            scoreSystem.OnGameOver += GameOver;
        }

        /// <summary>
        /// Start method, so that the inheriting classes can call it too.
        /// </summary>
        internal virtual void InitializeStart()
        {
            BallRb = GetComponent<Rigidbody>();
            playerController.OnApplyForce += (a) => lastShot = scoreSystem.CurShots + 1;
        }

        /// <summary>
        /// Shoots the ball.
        /// </summary>
        /// <param name="force"> Value of how much force will be applied to the ball. </param>
        public virtual void ShootBall(float force) { }

        private void OnCollisionEnter(Collision other)
        {
            if (OnHit != null)
                OnHit(gameObject, other.gameObject);

            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball"/*  && ScoreSystem.Instance.CurShots == lastShot */)
            {
                invoker.AddCommand(new BallHitCommand(this, transform.position, lastShot));
                lastShot = 0;
            }
        }

        /// <summary>
        /// Called when the game is over
        /// </summary>
        void GameOver()
        {
            Destroy(this);
        }
    }
}