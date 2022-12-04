using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Futures.Base;

namespace Futures.Util.ThreadSafe
{
    public class FutureWatcher
    {
        private readonly ConcurrentDictionary<IFuture, bool> _futures = new ConcurrentDictionary<IFuture, bool>(); 
        public IFuture[] Futures => _futures.Keys.ToArray();

        public int FuturesCount => _futures.Count;

        public void AddFuture(IFuture future)
        {
            if (future == null) return;

            if (_futures.TryAdd(future, true))
            {
                future.AddListener(InnerRemoveFuture);
            }
        }

        private void InnerRemoveFuture(IFuture future)
        {
            var spinWait = new SpinWait();

            for (;;)
            {
                if (_futures.TryRemove(future, out _))
                {
                    future.RemoveListener(InnerRemoveFuture);
                    break;
                }

                spinWait.SpinOnce();
            }
        }

        public void CancelFutures()
        {
            foreach (var pair in _futures)
            {
                pair.Key.RemoveListener(InnerRemoveFuture);
                pair.Key.Cancel();
            }
        }    
    }
}
