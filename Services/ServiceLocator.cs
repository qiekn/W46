﻿//Implementation from: https://medium.com/medialesson/simple-service-locator-for-your-unity-project-40e317aad307

using System;
using System.Collections.Generic;

namespace InfimaGames.LowPolyShooterPack
{
    // Simple service locator for <see cref="IGameService"/> instances.
    public class ServiceLocator
    {
        // Currently registered services.
        private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

        public static ServiceLocator Current { get; private set; }

        public static void Initialize() { Current = new ServiceLocator(); }

        // Gets the service instance of the given type.
        // <typeparam name="T">The type of the service to lookup.</typeparam>
        // <returns>The service instance.</returns>
        public T Get<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Log.kill($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }

        // Registers the service with the current service locator.
        // <typeparam name="T">Service type.</typeparam>
        // <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            string key = typeof(T).Name;
            if (services.ContainsKey(key))
            {
                Log.kill($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            //Add.
            services.Add(key, service);
        }

        // Unregisters the service from the current service locator.
        // <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Log.kill($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            services.Remove(key);
        }
    }
}
