using Services.Timers;
using Utils.Ioc;

namespace Futures.Base
{
    public abstract class TimerFuture : Future
    {
        protected ITimer timer;
        
        protected readonly ITimersService _timersService = IoC.Resolve<TimersService>();

        protected override void OnRun()
        {
        }
        
        protected override void OnComplete()
        {
            if (!IsCancelled) return;
            timer?.Dispose();
            timer = null;
        }
    }
}