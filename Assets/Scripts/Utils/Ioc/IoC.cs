using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Ioc
{
    public class IoC : Singleton<IoC>
    {
        private readonly Dictionary<Type, object> _types = new();

        public static object Register<T>(T type) where T : Type
        {
            var instance = Create(type);
            Instance._types[type] = instance;
            return instance;
        }

        public static void Register(object instance)
        {
            Instance._types[instance.GetType()] = instance;
        }

        public static void Register<T>()
        {
            Instance._types[typeof(T)] = Create<T>();
        }
        
        public static object Resolve<T>(T type)
        {
            return Instance._types.TryGetValue(type as Type ?? type.GetType(), out var o) ? o : Resolve<T>();
        }
        public static T Resolve<T>()  
        {
            if (Instance._types.TryGetValue(typeof(T), out var o))
            {
                return (T)o;
            }

            throw new ArgumentNullException($"{typeof(T)} not registrated in Ioc");
        }
        
        public static T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private static object Create<T>(T type) where T : Type
        {
            var defaultConstructor = type.GetConstructors()[0];
            var defaultParams = defaultConstructor.GetParameters();
            var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();

            return defaultConstructor.Invoke(parameters);
        }
    }
}