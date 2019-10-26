using UnityEngine;

namespace CaromBilliard
{
    public class IngameScoreSystem : ScoreSystem
    {
        private bool useTimer = true;

        private void Start()
        {
            Stats = new PlayerStats();
            RegisterEvents();
            ReplaySystem.OnReplayStart += UnregisterEvents;
            ReplaySystem.OnReplayStart += () => useTimer = false;
            ReplaySystem.OnReplayStop += RegisterEvents;
            ReplaySystem.OnReplayStop += () => useTimer = true;
        }

        void RegisterEvents()
        {
            PlayerController.OnApplyForce += ApplyShots;
            BallsManager.Instance.OnMoving += CheckMove;
            OnGameOver += GameOver;

            for (int i = 0; i < BallsManager.Instance.Balls.Length; i++)
                BallsManager.Instance.Balls[i].OnHit += GetPoints;
        }

        void UnregisterEvents()
        {
            PlayerController.OnApplyForce -= ApplyShots;
            BallsManager.Instance.OnMoving -= CheckMove;
            OnGameOver -= GameOver;

            for (int i = 0; i < BallsManager.Instance.Balls.Length; i++)
                BallsManager.Instance.Balls[i].OnHit -= GetPoints;
        }

        void ApplyShots(float f)
        {
            CurShots++;
        }

        private void Update()
        {
            Timer();
        }

        void Timer()
        {
            if (useTimer)
                CurTimer += Time.deltaTime;
        }

        private GameObject preHitObject;

        void GetPoints(GameObject source, GameObject target)
        {
            if (source.tag == "Player" && target.tag == "Ball")
            {
                if (preHitObject != null && target != preHitObject)
                    CurScore++;
                preHitObject = target;
            }
        }

        void CheckMove(bool isMoving)
        {
            if (!isMoving)
                preHitObject = null;
        }

        void GameOver()
        {
            useTimer = false;
            SaveStats();
        }
    }
}
