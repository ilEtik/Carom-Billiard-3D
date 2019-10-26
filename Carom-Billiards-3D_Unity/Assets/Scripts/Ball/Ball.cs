using UnityEngine;
using System;

namespace CaromBilliard
{
    public class Ball : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody BallRb;
        public CommandInvoker invoker;

        public bool IsMoving { get { return BallRb.velocity.magnitude > .1f; } }

        public event Action<GameObject, GameObject> OnHit;

        public int lastShot;

        private void Start()
        {
            Init();
        }

        public virtual void Init()
        {
            BallRb = GetComponent<Rigidbody>();
            invoker = FindObjectOfType<CommandInvoker>();
            IngameScoreSystem.Instance.OnGameOver += GameOver;
            PlayerController.OnApplyForce += (a) => lastShot = ScoreSystem.Instance.CurShots + 1;
        }

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

        void GameOver()
        {
            Destroy(this);
        }
    }
}