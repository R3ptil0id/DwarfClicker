using Futures.Base;
using Services.Timers;
using Utils.Ioc;

namespace Futures
{
    public class DelayFuture : Future
    {
        private readonly ITimersService _timersService = IoC.Resolve<TimersService>();
        
        private float _delay;
        private IFuture _delayedFuture;
        private ITimer _timer;
        private bool _realtime;

        public IFuture Initialize(float delay, bool realtime, IFuture delayedFuture)
        {
            _delay = delay;
            _realtime = realtime;
            _delayedFuture = delayedFuture;
            return this;
        }
        
        protected override void OnRun()
        {
            _delayedFuture.AddListener(f =>
            {
                if (f.IsDone)
                {
                    Complete();
                }
            });

            _timer = _timersService.AddTimer(_delay, null, (_) =>
            {
                _delayedFuture.Run();
            });
        }

        protected override void OnComplete()
        {
            if (IsCancelled)
            {
                _timersService.RemoveTimer(_timer);
                _delayedFuture.Cancel();
            }

            _timer = null;
            _delayedFuture = null;
            _delay = 0;
        }
    }
}
