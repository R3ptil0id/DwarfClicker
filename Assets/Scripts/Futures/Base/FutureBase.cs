using System;

namespace Futures.Base
{
    public abstract class FutureBase : IFuture
    {
        public bool IsCancelled { get; protected set; }
        public bool IsDone { get; protected set; }
        public bool WasRun { get; protected set; }

        private event Action<IFuture> Completed;
        private event Action<IFuture> Finalized;
        private event Action<IFuture> Started;
        protected bool promise;

        protected void CallRunHandlers()
        {
            Started?.Invoke(this);
            Started = null;
        }

        protected void CallHandlers()
        {
            Completed?.Invoke(this);
            Completed = null;
        }
        
        protected void CallFinalizeHandlers()
        {
            Finalized?.Invoke(this);
            Finalized = null;
        }

        public IFuture AddListenerOnRun(Action<IFuture> method)
        {
            if (!WasRun)
            {
                Started += method;
            }
            else
            {
                method(this);
            }

            return this;
        }

        public void RemoveListenerOnRun(Action<IFuture> method)
        {
            Started -= method;
        }

        public IFuture AddListener(Action<IFuture> method)
        {
            if (!IsDone && !IsCancelled)
            {
                Completed += method;
            }
            else
            {
                method(this);
            }

            return this;
        }


        public void RemoveListener(Action<IFuture> method)
        {
            Completed -= method;
        }
        
        public IFuture AddListenerOnFinalize(Action<IFuture> method)
        {
            if (!IsDone && !IsCancelled)
            {
                Finalized += method;
            }
            else
            {
                method(this);
            }

            return this;
        }

        public void RemoveListenerOnFinalize(Action<IFuture> method)
        {
            Finalized -= method;
        }

        public abstract void Cancel();
        public abstract IFuture Run();

        public static T StaticCast<T>(IFuture future)
        {
            return (T)future;
        }

        public T Cast<T>() where T : IFuture
        {
            return StaticCast<T>(this);
        }

        public abstract bool Reuse();

        protected void SetAsPromise()
        {
            promise = true;
        }
    }
}
