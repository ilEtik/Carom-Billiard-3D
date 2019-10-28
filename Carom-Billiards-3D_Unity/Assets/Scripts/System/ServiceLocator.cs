using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace CaromBilliard
{
    /// <summary>
    /// Class for locating all services that are needed from different classes.
    /// </summary>
    public class ServiceLocator : MonoBehaviour
    {
        private static Dictionary<object, object> serviceDictionary = new Dictionary<object, object>();

        private void Awake()
        {
            var services = FindObjectsOfType<MonoBehaviour>().OfType<IServiceLocator>();

            foreach (IServiceLocator service in services)
                service.ProvideService();

            foreach (IServiceLocator service in services)
                service.GetService();
        }

        /// <summary>
        /// Adds a new service.
        /// </summary>
        /// <param name="service"> The Name of the service. </param>
        /// <typeparam name="T"> The Service itself. </typeparam>
        public static void ProvideService<T>(T service)
        {
            if (serviceDictionary.ContainsKey(typeof(T)))
                serviceDictionary.Remove(typeof(T));
            serviceDictionary.Add(typeof(T), service);
        }

        /// <summary>
        /// Receive a service.
        /// </summary>
        /// <typeparam name="T"> The service that is needed. </typeparam>
        /// <returns> Returns the Service that is needed. </returns>
        public static T GetService<T>()
        {
            if (serviceDictionary.ContainsKey(typeof(T)))
                return (T)serviceDictionary[typeof(T)];
            else
                throw new ApplicationException("No Service of type " + typeof(T) + " found.");
        }
    }
}
