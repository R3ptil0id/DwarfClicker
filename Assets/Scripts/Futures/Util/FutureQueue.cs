using System;
using System.Collections.Generic;
using Futures.Base;

namespace Futures.Util
{
    public class FutureQueue : IFutureContainer
    {
        private readonly Queue<IFuture> _queueFutures = new Queue<IFuture>();
        private IFuture _current;
        public int FuturesCount => _queueFutures.Count;
        public event Action<IFuture> FutureCompleted;
        public event Action AllFutureCompleted;
        
        public void AddFuture(IFuture future)
        {
            if (future.IsDone || future.IsCancelled || future.WasRun)
            {
                throw new Exception("future already run or completed");
            }

            _queueFutures.Enqueue(future);
            future.AddListener(FutureComplete);

            if (_queueFutures.Count != 1) return;
            
            _current = future;
            future.Run();
        }

        private void FutureComplete(IFuture f)
        {
            _queueFutures.Dequeue();
            _current = null;
            
            if (_queueFutures.Count > 0)
            {
                _current = _queueFutures.Peek();
            }
            else
            {
                AllFutureCompleted?.Invoke();
            }
            FutureCompleted?.Invoke(f);
            _current?.Run();
        }

        public void CancelCurrent()
        {
            _current?.Cancel();
        }

        public void Cancel()
        {
            foreach (var future in new List<IFuture>(_queueFutures))
            {
                future.RemoveListener(FutureComplete);
                future.Cancel();
            }
            
            _queueFutures.Clear();
        }
    }
}
