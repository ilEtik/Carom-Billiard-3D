
namespace CaromBilliard
{
    public abstract class Command
    {
        public Ball Motor { get; private set; }

        public Command (Ball motor)
        {
            Motor = motor;
        }

        public abstract void Execute();
        public abstract void Undo();
    }
}
