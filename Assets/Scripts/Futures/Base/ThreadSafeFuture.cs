using System;

namespace Futures.Base
{
    public abstract class ThreadSafeFuture : IFuture
    {
        private readonly object _syncRoot;
        public bool IsCancelled { get; private set; }
        public bool IsDone { get; private set; }
        public bool WasRun { get; private set; }

        private event Action<IFuture> Completed;
        private event Action<IFuture> Finalized;
        private event Action<IFuture> Started;
        private volatile bool _promise;

        protected ThreadSafeFuture ()
        {
            _syncRoot = new object();
        }

        private void CallRunHandlers()
        {
            Started?.Invoke(this);
            Started = null;
        }

        private void CallHandlers()
        {
            Completed?.Invoke(this);
            Completed = null;
        }

        private void CallFinalizeHandlers()
        {
            Finalized?.Invoke(this);
            Finalized = null;
        }

        public IFuture AddListenerOnRun(Action<IFuture> method)
        {
            bool call = false;
            lock (_syncRoot)
            {
                if (!WasRun)
                    Started += method;
                else
                    call = true;
            }

            if (call)
            {
                method(this);
            }

            return this;
        }

        public void RemoveListenerOnRun(Action<IFuture> method)
        {
            lock (_syncRoot)
            {
                Started -= method;
            }
        }

        public IFuture AddListener(Action<IFuture> method)
        {
            bool call = false;
            lock (_syncRoot)
            {
                if (!IsDone && !IsCancelled)
                    Completed += method;
                else
                    call = true;
            }

            if (call)
                method(this);

            return this;
        }

        public void RemoveListener(Action<IFuture> method)
        {
            lock (_syncRoot)
                Completed -= method;
        }
        
        public IFuture AddListenerOnFinalize(Action<IFuture> method)
        {
            bool call = false;
            lock (_syncRoot)
            {
                if (!IsDone && !IsCancelled)
                    Finalized += method;
                else
                    call = true;
            }

            if (call)
                method(this);

            return this;
        }

        public void RemoveListenerOnFinalize(Action<IFuture> method)
        {
            lock (_syncRoot)
                Finalized -= method;
        }

        public void Cancel()
        {
            lock (_syncRoot)
            {
                if (_promise || IsCancelled || IsDone) return;
                IsCancelled = true;
            }

            OnComplete();
            CallHandlers();
            CallFinalizeHandlers();
        }

        public void Complete()
        {
            lock (_syncRoot)
            {
                if (IsCancelled || IsDone) return;
                IsDone = true;
            }

            OnComplete();
            CallHandlers();
            CallFinalizeHandlers();
        }

        public IFuture Run()
        {
            lock (_syncRoot)
            {
                if (WasRun || IsCancelled || IsDone) return this;
                WasRun = true;
            }

            OnRun();
            CallRunHandlers();
            return this;
        }

        protected abstract void OnRun();
        protected abstract void OnComplete();

        public static T StaticCast<T>(IFuture future)
        {
            return (T)future;
        }

        public T Cast<T>() where T : IFuture
        {
            return StaticCast<T>(this);
        }

        public virtual bool Reuse()
        {
            lock (_syncRoot)
            {
                if (!IsDone && !IsCancelled) return false;
            
                IsCancelled = false;
                IsDone = false;
                WasRun = false;
                return true;
            }
        }

        protected void SetAsPromise()
        {
            _promise = true;
        }
    }
}
