using System;
using UnityEngine;
#if UNITY_5_3_OR_NEWER

#endif

namespace Futures.Base
{
    public class FutureTask<T> : ThreadSafeFuture
    {
        public T Result { get; private set; }
        
        private readonly Func<T> _func;
        
        public FutureTask(Func<T> func)
        {
            _func = func;
        }

        protected override void OnRun()
        {
            try
            {
                Result = _func();
            }
            catch (Exception e)
            {
#if UNITY_5_3_OR_NEWER
                Debug.LogException(e);
#endif
                throw;
            }
            
            Complete();
        }

        protected override void OnComplete()
        {
        }
    }
    
    public class FutureTask : ThreadSafeFuture
    {
        private Action _action;

        public FutureTask()
        {
            //no op
        }
        
        public FutureTask(Action action)
        {
            _action = action;
        }

        public FutureTask Initialize(Action action)
        {
            _action = action;
            return this;
        }

        protected override void OnRun()
        {
            try
            {
                _action?.Invoke();
            }
            catch (Exception e)
            {
#if UNITY_5_3_OR_NEWER                
                Debug.LogException(e);
#endif
                throw;
            }
            
            Complete();
        }

        protected override void OnComplete()
        {
        }
    }
}