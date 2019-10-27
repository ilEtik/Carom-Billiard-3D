namespace CaromBilliard
{
    public interface IServiceLocator
    {
        void ProvideService();
        void GetService();
    }
}