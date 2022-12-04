using System;
using Controllers.GameController;

namespace Services.Timers
{
    public interface ITimersService : IUpdateListener
    {
        event Action<ITimer> Added; 
        event Action<ITimer> Removed;

        void Run();

        ITimer AddTimer(float duration, Action<ITimer> tick = null, Action<ITimer> done = null);
        void RemoveTimer(ITimer timer);
        void AddTimer(float duration, bool realtime, out ITimer timer);
        bool TryAddTimer(ITimer timer);
    }
}