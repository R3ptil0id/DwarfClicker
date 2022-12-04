using System;
using System.Collections.Generic;
using Futures.Base;

namespace Futures.Util
{
    public static class FuturePool
    {
        private static readonly Dictionary<Type, Queue<IFuture>> _pool = new Dictionary<Type, Queue<IFuture>>();

        public static T Take<T>() where T : class, IFuture, new()
        {
            var type = typeof(T);
            if (!_pool.TryGetValue(type, out var list))
            {
                _pool.Add(type, list = new Queue<IFuture>());
            }

            T future;
            if (list.Count > 0)
            {
                future = (T) list.Dequeue();
                future.Reuse();
            }
            else
            {
                future = new T();
            }

            future.AddListenerOnFinalize(Return<T>);
            
            return future;
        }

        public static void Free()
        {
            _pool.Clear();
        }

        private static void Return<T>(IFuture future) where T : class, IFuture
        {
            _pool[typeof(T)].Enqueue(future);
        }
    }
}