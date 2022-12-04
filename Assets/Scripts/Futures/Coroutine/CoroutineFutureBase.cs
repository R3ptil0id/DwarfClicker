using System;
using System.Collections;
using Futures.Base;

namespace Futures.Coroutine
{
    public abstract class CoroutineFutureBase : Base.Future
    {
        protected IEnumerator enumerator;
        protected IFuture currentFuture;

        protected void Next(IFuture obj)
        {
            if (IsCancelled) return;
            
            if (enumerator.MoveNext())
            {
                if (enumerator?.Current != null)
                {
                    currentFuture = (IFuture)enumerator.Current;
                    currentFuture.AddListener(Next);
                }
            }
            else
                Complete();
        }

        protected override void OnComplete()
        {
            if (IsCancelled)
            {
                if (currentFuture != null)
                {
                    currentFuture.RemoveListener(Next);
                    currentFuture.Cancel();
                }
            }

            currentFuture = null;
            enumerator = null;
        }

        public override bool Reuse()
        {
            throw new NotSupportedException();
        }
    }
}
