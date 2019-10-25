using System.Collections.Generic;
using UnityEngine;

namespace CaromBilliard
{
    public class CommandInvoker : MonoBehaviour
    {
        public List<Command> commands = new List<Command>();
        private int curCommandInd = 0;
        private int CurCommandInd
        {
            get { return curCommandInd; }
            set
            {
                if (value < 0)
                    curCommandInd = 0;
                else if (value > commands.Count - 1)
                    curCommandInd = commands.Count - 1;
                else
                    curCommandInd = value;
            }
        }

        private void Start()
        {
            ReplaySystem.OnReplayStop += (a) => CurCommandInd = commands.Count;
        }

        public void ExecuteCommand(Command command)
        {
            commands.Add(command);
            command.Execute();
            CurCommandInd++;
        }

        public void UndoCommand()
        {
            if (commands.Count <= 0)
                return;

            commands[CurCommandInd].Undo();
            CurCommandInd--;
        }

        public void RedoCommand()
        {
            if (commands.Count <= 0)
                return;

            commands[CurCommandInd].Execute();
            CurCommandInd++;
        }
    }
}
