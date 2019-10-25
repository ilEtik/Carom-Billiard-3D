using System.Collections;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class ReplaySystem : MonoBehaviour
    {
        private CommandInvoker invoker;

        public static event Action<bool> OnReplayStart;
        public static event Action<bool> OnReplayStop;

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
                OnReplayStop(false);
            }
        }

        public void StartReplay()
        {
            StartCoroutine(Replay());
        }

        IEnumerator Replay()
        {
            if(OnReplayStart != null)
                OnReplayStart(true);

            for (int i = invoker.commands.Count - 1; i >= 0; i--)
            {
                invoker.UndoCommand();

                if (invoker.commands[i].GetType() == typeof(ShootBallCommand))
                    break;
            }

            invoker.RedoCommand();

            yield return new WaitForSeconds(.1f);

            BallsManager.Instance.OnMoving += StopReplay;
        }
    }
}
