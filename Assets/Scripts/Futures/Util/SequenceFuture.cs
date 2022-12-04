using System.Collections.Generic;
using Futures.Base;

namespace Futures.Util
{
    public class SequenceFuture : FutureBase, IFutureCollection
    {
        private readonly List<IFuture> _futures = new List<IFuture>();
        
        public int FuturesCount => _futures.Count;
        
        public List<IFuture> GetFuturesCopy()
        {
            return new List<IFuture>(_futures);
        }
        
        public SequenceFuture(IEnumerable<IFuture> futures)
        {
            foreach (var future in futures)
            {
                if (future == null) continue;
                AddFuture(future);
            }
        }

        public SequenceFuture(params IFuture[] futures)
        {
            foreach (var future in futures)
            {
                if (future == null) continue;
                AddFuture(future);
            }
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

            if (_futures.Count > 0)
            {
                _futures[0].Run();
                _futures[0].AddListener(OnFutureComplete);
                return;
            }
            
            IsDone = true;
            WasRun = false;

            CallHandlers();
            CallFinalizeHandlers();
        }

        public override IFuture Run()
        {
            if (WasRun) return this;
            WasRun = true;
            CallRunHandlers();
            IsDone = _futures.Count == 0;
            
            if (IsDone)
            {
                WasRun = false;
                CallHandlers();
                CallFinalizeHandlers();
            }
            else
            {
                _futures[0].Run();
                _futures[0].AddListener(OnFutureComplete);

            }
            
            return this;
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

            var copy = GetFuturesCopy();
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
    }
}