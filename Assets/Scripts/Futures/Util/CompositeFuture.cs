using System.Collections.Generic;
using Futures.Base;

namespace Futures.Util
{
    public class CompositeFuture : FutureBase, IFutureCollection
    {
        public int FuturesCount => _futures.Count;

        private readonly List<IFuture> _futures = new List<IFuture>();

        public CompositeFuture(IEnumerable<IFuture> futures)
        {
            foreach (var future in futures)
            {
                if (future == null) continue;
                AddFuture(future);
            }
        }

        public CompositeFuture(params IFuture[] futures)
        {
            foreach (var future in futures)
            {
                if (future == null) continue;
                AddFuture(future);
            }
        }
        
        public override bool Reuse()
        {
            if (!IsDone && !IsCancelled) return false;

            IsCancelled = false;
            IsDone = false;
            WasRun = false;
            return true;
        }
        
        public override void Cancel()
        {
            if (promise || IsCancelled || IsDone) return;
            
            IsCancelled = true;
            WasRun = false;
            var copy = GetFuturesCopyList();
            _futures.Clear();

            foreach (var future in copy)
            {
                if (future.IsCancelled) continue;
                
                future.RemoveListener(OnFutureComplete);
                future.Cancel();
            }

            CallHandlers();
            CallFinalizeHandlers();
        }

        public void AddFuture(IFuture future)
        {
            if (WasRun || IsDone || IsCancelled || future.IsDone || future.IsCancelled) return;

            _futures.Add(future);
        }

        private void OnFutureComplete(IFuture future)
        {
            _futures.Remove(future);
            future.RemoveListener(OnFutureComplete);

            if (_futures.Count > 0) return;
            
            IsDone = true;
            WasRun = false;

            CallHandlers();
            CallFinalizeHandlers();
        }

        public override IFuture Run()
        {
            if (WasRun) return this;
            
            WasRun = true;
            IsDone = _futures.Count == 0;

            CallRunHandlers();

            if (IsDone)
            {
                WasRun = false;
                CallHandlers();
                CallFinalizeHandlers();
            }
            else
            {
                foreach (var future in GetFuturesCopyList())
                {
                    future.Run();
                    future.AddListener(OnFutureComplete);
                }
            }
            return this;
        }
        
        private List<IFuture> GetFuturesCopyList()
        {
            return new List<IFuture>(_futures);
        }
    }
}
