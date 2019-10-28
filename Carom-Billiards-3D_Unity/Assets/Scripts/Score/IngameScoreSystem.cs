using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Manages the stats that the player reaches during playing.
    /// </summary>
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
        }

        /// <summary>
        /// Register all events that are needed for updating the score.
        /// </summary>
        void RegisterEvents()
        {
            playerController.OnApplyForce += ApplyShots;
            ballsManager.OnMoving += CheckMove;
            OnGameOver += GameOver;

            for (int i = 0; i < ballsManager.Balls.Length; i++)
                ballsManager.Balls[i].OnHit += GetScore;

            replaySystem.OnReplayStart += UnregisterEvents;
            replaySystem.OnReplayStart += () => useTimer = false;
            replaySystem.OnReplayStop += RegisterEvents;
            replaySystem.OnReplayStop += () => useTimer = true;
            playerController.OnApplyForce += (a) => getScore =false;
        }

        /// <summary>
        /// Unregister all events.
        /// </summary>
        void UnregisterEvents()
        {
            playerController.OnApplyForce -= ApplyShots;
            ballsManager.OnMoving -= CheckMove;
            OnGameOver -= GameOver;

            for (int i = 0; i < ballsManager.Balls.Length; i++)
                ballsManager.Balls[i].OnHit -= GetScore;
        }

        /// <summary>
        /// Increases the current shots.
        /// </summary>
        /// <param name="force"> The value of how much force was added to the ball. Just here for the event. </param>
        void ApplyShots(float force)
        {
            CurShots++;
        }

        private void Update()
        {
            Timer();
        }

        /// <summary>
        /// Sets the timer of how long the player is playing the current round.
        /// </summary>
        void Timer()
        {
            if (useTimer)
                CurTimer += Time.deltaTime;
        }

        private GameObject preHitObject;
        private bool getScore;

        /// <summary>
        /// Increases the score of the player.
        /// </summary>
        /// <param name="source"> The object that hitted something. </param>
        /// <param name="target"> The object that was hitted. </param>
        void GetScore(GameObject source, GameObject target)
        {
            if (source.tag == "Player" && target.tag == "Ball")
            {
                if (preHitObject != null && target != preHitObject)
                {
                    CurScore++;
                    getScore = true;
                }
                preHitObject = target;
            }
        }

        /// <summary>
        /// Checks if any of the balls are moving.
        /// </summary>
        /// <param name="isMoving"> If the balls are moving. </param>
        void CheckMove(bool isMoving)
        {
            if (!isMoving)
                preHitObject = null;
        }

        /// <summary>
        /// Called when the game is over.
        /// </summary>
        void GameOver()
        {
            useTimer = false;
            SaveStats();
        }
    }
}
