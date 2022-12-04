namespace Futures.Base
{
    public abstract class Future : FutureBase
    {
        public override void Cancel()
        {
            if (promise || IsCancelled || IsDone) return;
            
            IsCancelled = true;
            OnComplete();
            CallHandlers();
            CallFinalizeHandlers();
        }

        public void Complete()
        {
            if (IsCancelled || IsDone) return;
            
            IsDone = true;
            OnComplete();
            CallHandlers();
            CallFinalizeHandlers();
        }

        public override IFuture Run()
        {
            if (WasRun || IsCancelled || IsDone) return this;
            
            WasRun = true;
            OnRun();
            CallRunHandlers();
            
            return this;
        }

        protected abstract void OnRun();
        protected abstract void OnComplete();
        
        public override bool Reuse()
        {
            if (!IsDone && !IsCancelled) return false;
            
            IsCancelled = false;
            IsDone = false;
            WasRun = false;
            return true;
        }
    }
}
