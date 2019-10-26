using System.Collections;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class ReplaySystem : MonoBehaviour
    {
        private CommandInvoker invoker;

        public static event Action OnReplayStart;
        public static event Action OnReplayStop;

        private void Start()
        {
            invoker = FindObjectOfType<CommandInvoker>();
        }

        void StopReplay(bool isMoving)
        {
            if (!isMoving)
            {
                StopAllCoroutines();
                BallsManager.Instance.OnMoving -= StopReplay;
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

            BallsManager.Instance.OnMoving += StopReplay;
        }
    }
}
