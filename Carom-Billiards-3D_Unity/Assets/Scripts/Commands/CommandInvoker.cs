using System.Collections.Generic;
using UnityEngine;
using System;

namespace CaromBilliard
{
    public class CommandInvoker : MonoBehaviour
    {
        public const int MaxCommands = 16;
        public Command[] commands = new Command[MaxCommands];
        private int head = 0;
        private int tail = 0;

        private int _undoTail;
        public int UndoTail
        {
            get { return _undoTail; }
            set
            {
                if (value < 0)
                    _undoTail = MaxCommands - 1;
                else
                    _undoTail = value;
            }
        }

        private int _undoHead;
        public int UndoHead
         {
            get { return _undoHead; }
            set
            {
                if (value < 0)
                    _undoHead = MaxCommands - 1;
                else
                    _undoHead = value;
            }
        }

        public static event Action<bool> OnHasCommands;

        private void Start()
        {
            OnHasCommands(false);
            ReplaySystem.OnReplayStop += () => UndoTail = (tail - 1) % MaxCommands;
            ReplaySystem.OnReplayStart += () => UndoHead = head;
        }

        private void Update()
        {
            ExecuteCommands();
        }

        void ExecuteCommands()
        {
            if (head == tail)
                return;

            commands[head].Execute();
            head = (head + 1) % MaxCommands;
            UndoHead = head;
        }

        public void AddCommand(Command command)
        {
            commands[tail] = command;
            UndoTail = tail;
            tail = (tail + 1) % MaxCommands;
            OnHasCommands(true);
        }

        public void UndoCommand()
        {
            commands[UndoTail].Undo();
            UndoTail = (UndoTail - 1) % MaxCommands;
            UndoHead = (UndoHead - 1) % MaxCommands;
        }

        public void RedoCommand()
        {
            commands[UndoHead].Execute();
        }
    }
}
