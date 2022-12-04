using System;
using Controllers.GameController;
using Futures.Base;
using Services.GameLoop;

namespace Futures
{
    public class WaitWhileFuture : Future, IUpdateListener
    {
        private readonly GameLoopService _gameLoopService = GameLoopService.Instance;
        private Func<bool> _predicate;
        
        // public event Action<IDisposableObject> OnDisposed;
        public bool IsDisposed { get; private set; }

        public WaitWhileFuture()
        {
            //no op
        }

        public WaitWhileFuture Initialize(Func<bool> p)
        {
            _predicate = p;
            return this;
        }
        
        protected override void OnRun()
        {
            _gameLoopService.Register(this);
        }

        protected override void OnComplete()
        {
            _predicate = null;
            _gameLoopService.Unregister(this);
        }

        public void Dispose()
        {
            IsDisposed = true;
        }

        public override bool Reuse()
        {
            IsDisposed = false;
            return base.Reuse();
        }

        public void Update(float deltaTime)
        {
            if (_predicate())
            {
                Complete();
            }
        }
    }
}