namespace CaromBilliard
{
    /// <summary>
    /// Interface that implements methods when an object requires an object from the ServiceLocator class.
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// Applies services to the ServiceLocator.
        /// </summary>
        void ProvideService();
        /// <summary>
        /// Receive services from the ServiceLocator.
        /// </summary>
        void GetService();
    }
}