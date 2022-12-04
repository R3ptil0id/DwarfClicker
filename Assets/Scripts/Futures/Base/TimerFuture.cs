using Services.Timers;

namespace Futures.Base
{
    public abstract class TimerFuture : Future
    {
        protected ITimer timer;
        
        protected readonly ITimersService timersService = TimersService.Instance;

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