using UnityEngine;

namespace CaromBilliard
{
    /// <summary>
    /// Base class for all commands.
    /// </summary>
    public abstract class Command
    {
        public Ball Motor { get; private set; }

        public Command (Ball motor)
        {
            Motor = motor;
        }

        /// <summary>
        /// Method that executes the command.
        /// </summary>
        public abstract void Execute();
        /// <summary>
        /// Mehtod that undo the command. 
        /// </summary>
        public abstract void Undo();
    }
}
