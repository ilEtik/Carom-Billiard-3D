using UnityEngine;

namespace CaromBilliard
{
    public class IngameScoreSystem : ScoreSystem
    {
        public override void ProvideService()
        {
            base.ProvideService();
            ServiceLocator.ProvideService(this);
        }

        private ReplaySystem replaySystem;
        private PlayerController playerController;
        private BallsManager ballsManager;

        public override void GetService()
        {
            base.GetService();
            replaySystem = ServiceLocator.GetService<ReplaySystem>();
            playerController = ServiceLocator.GetService<PlayerController>();
            ballsManager = ServiceLocator.GetService<BallsManager>();
        }

        private bool useTimer = true;

        private void Start()
        {
            Stats = new PlayerStats();
            RegisterEvents();
            replaySystem.OnReplayStart += UnregisterEvents;
            replaySystem.OnReplayStart += () => useTimer = false;
            replaySystem.OnReplayStop += RegisterEvents;
            replaySystem.OnReplayStop += () => useTimer = true;
        }

        void RegisterEvents()
        {
            playerController.OnApplyForce += ApplyShots;
            ballsManager.OnMoving += CheckMove;
            OnGameOver += GameOver;

            for (int i = 0; i < ballsManager.Balls.Length; i++)
                ballsManager.Balls[i].OnHit += GetPoints;
        }

        void UnregisterEvents()
        {
            playerController.OnApplyForce -= ApplyShots;
            ballsManager.OnMoving -= CheckMove;
            OnGameOver -= GameOver;

            for (int i = 0; i < ballsManager.Balls.Length; i++)
                ballsManager.Balls[i].OnHit -= GetPoints;
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
