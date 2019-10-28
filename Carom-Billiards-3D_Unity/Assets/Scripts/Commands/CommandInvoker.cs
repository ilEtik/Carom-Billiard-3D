using System.Collections.Generic;
using UnityEngine;
using System;

namespace CaromBilliard
{
    /// <summary>
    /// Calls all commands.
    /// </summary>
    public class CommandInvoker : MonoBehaviour, IServiceLocator
    {
        void IServiceLocator.ProvideService()
        {
            ServiceLocator.ProvideService(this);
        }
        
        private ReplaySystem replaySystem;

        void IServiceLocator.GetService()
        {
            replaySystem = ServiceLocator.GetService<ReplaySystem>();
        }

        public const int MaxCommands = 32;
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

        public event Action<bool> OnHasCommands;


        private void Start()
        {
            if (OnHasCommands != null)
                OnHasCommands(false);
                
            replaySystem.OnReplayStop += () => UndoTail = (tail - 1) % MaxCommands;
            replaySystem.OnReplayStart += () => UndoHead = head;
            
        }

        private void Update()
        {
            ExecuteCommands();
        }

        /// <summary>
        /// Executes all command insider of the commands array.
        /// </summary>
        void ExecuteCommands()
        {
            if (head == tail)
                return;

            commands[head].Execute();
            head = (head + 1) % MaxCommands;
            UndoHead = head;
        }

        /// <summary>
        /// Adds new command to the commands array.
        /// </summary>
        /// <param name="command"> the command that should be executed. </param>
        public void AddCommand(Command command)
        {
            commands[tail] = command;
            UndoTail = tail;
            tail = (tail + 1) % MaxCommands;
            OnHasCommands(true);
        }

        /// <summary>
        /// Undo an command.
        /// </summary>
        public void UndoCommand()
        {
            if(commands[UndoTail] == null)
                return;

            commands[UndoTail].Undo();
            UndoTail = (UndoTail - 1) % MaxCommands;
            UndoHead = (UndoHead - 1) % MaxCommands;
        }

        /// <summary>
        /// Redo the last command that was undid.
        /// </summary>
        public void RedoCommand()
        {
            if(commands[UndoHead] == null)
                return;
                
            commands[UndoHead].Execute();
        }
    }
}
