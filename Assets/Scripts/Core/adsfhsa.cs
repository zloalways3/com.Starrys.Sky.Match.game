using System;
using System.Collections.Generic;
using Core.Api;

namespace Core
{
    public static class adsfhsa
    {
        private static Dictionary<Type, dfghsjsdf> _services = new Dictionary<Type, dfghsjsdf>();

        public static void Bind<T>(T service) where T : class, dfghsjsdf
        {
            if (_services.ContainsKey(typeof(T)))
                return;

            _services[typeof(T)] = service;
        }

        public static T Get<T>() where T : class, dfghsjsdf => 
            _services.ContainsKey(typeof(T)) ? (T)_services[typeof(T)] : null;
    }
}