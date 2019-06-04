using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Suzianna.Core
{
    public static class ServiceLocator
    {
        private static IDictionary<Type, object> _services;

        static ServiceLocator()
        {
            _services = new ConcurrentDictionary<Type, object>();
        }

        public static T GetService<T>()
        {
            _services.TryGetValue(typeof(T), out var result);
            if (result != null)
                return (T) result;
            return default(T);
        }

        public static void AddOrUpdate<TBaseType>(object implementation)
        {
            _services.Remove(typeof(TBaseType));
            _services.Add(typeof(TBaseType),implementation);
        }

        public static void Remove<T>()
        {
            _services.Remove(typeof(T));
        }

        public static void RemoveAll()
        {
            _services=new ConcurrentDictionary<Type, object>();
        }
    }
}