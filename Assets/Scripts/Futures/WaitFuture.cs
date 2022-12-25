using Futures.Base;
using Services.Timers;
using Utils.Ioc;

namespace Futures
{
    public class WaitFuture : Future
    {
        private float _duration;
        private ITimer _waitTimer;
        private bool _realtime;

        private readonly ITimersService _timersService = IoC.Resolve<TimersService>();// TimersService.Instance;

        public IFuture Initialize(float duration)
        {
            _duration = duration;
            return this;
        }

        protected override void OnRun()
        {
            _waitTimer = _timersService.AddTimer(_duration, null , (_) =>
            {
                Complete();
            });
        }

        protected override void OnComplete()
        {
            _timersService.RemoveTimer(_waitTimer);
            _waitTimer = null;
        }
    }
}
