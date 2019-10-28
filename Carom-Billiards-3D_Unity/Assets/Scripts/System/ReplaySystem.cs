using System.Collections;
using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Controlls the replay
    /// </summary>
    public class ReplaySystem : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService()
        {
            ServiceLocator.ProvideService(this);
        }

        private CommandInvoker invoker;
        private BallsManager ballsManager;

        void IServiceLocator.GetService()
        {
            invoker = ServiceLocator.GetService<CommandInvoker>();
            ballsManager = ServiceLocator.GetService<BallsManager>();
        }

        public event Action OnReplayStart;
        public event Action OnReplayStop;

        /// <summary>
        /// Starts the replay.
        /// </summary>
        public void StartReplay()
        {
            StartCoroutine(Replay());
        }

        /// <summary>
        /// Does the Replay. Undoes the Commands until the command was found where the player shooted the ball and redoes that.
        /// </summary>
        /// <returns></returns>
        IEnumerator Replay()
        {
            if (OnReplayStart != null)
                OnReplayStart();

            for (int i = invoker.UndoTail; i > -1; i = invoker.UndoTail)
            {
                invoker.UndoCommand();

                if (invoker.commands[i].GetType() == typeof(ShootBallCommand))
                {
                    invoker.RedoCommand();
                    break;
                }
            }

            yield return new WaitForSeconds(.1f);

            ballsManager.OnMoving += StopReplay;
        }

        /// <summary>
        /// Stops the replay when no balls are moving.
        /// </summary>
        /// <param name="isMoving"> Are the balls moving? </param>
        void StopReplay(bool isMoving)
        {
            if (!isMoving)
            {
                StopAllCoroutines();
                ballsManager.OnMoving -= StopReplay;
                OnReplayStop();
            }
        }
    }
}
