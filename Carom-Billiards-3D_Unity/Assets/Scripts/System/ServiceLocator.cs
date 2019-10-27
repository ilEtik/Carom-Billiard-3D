using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace CaromBilliard
{
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

        public static void ProvideService<T>(T service)
        {
            if (serviceDictionary.ContainsKey(typeof(T)))
                serviceDictionary.Remove(typeof(T));
            serviceDictionary.Add(typeof(T), service);
        }

        public static T GetService<T>()
        {
            if (serviceDictionary.ContainsKey(typeof(T)))
                return (T)serviceDictionary[typeof(T)];
            else
                throw new ApplicationException("No Service of type " + typeof(T) + " found.");
        }
    }
}
