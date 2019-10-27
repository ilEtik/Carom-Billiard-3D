using System.Collections;
using UnityEngine;
using System;

namespace CaromBilliard
{
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

        void StopReplay(bool isMoving)
        {
            if (!isMoving)
            {
                StopAllCoroutines();
                ballsManager.OnMoving -= StopReplay;
                OnReplayStop();
            }
        }

        public void StartReplay()
        {
            StartCoroutine(Replay());
        }

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
    }
}
