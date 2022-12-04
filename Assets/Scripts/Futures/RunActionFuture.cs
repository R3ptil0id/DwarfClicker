using System;

namespace Futures
{
    public class RunActionFuture : Futures.Base.Future
    {
        private Action _action;
        
        public RunActionFuture Initialize(Action action)
        {
            _action = action;
            return this;
        }
        
        protected override void OnRun()
        {
            _action?.Invoke();
            Complete();
        }

        protected override void OnComplete()
        {
            _action = null;
        }
    }
}