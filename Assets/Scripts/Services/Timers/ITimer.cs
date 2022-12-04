using System;

namespace Services.Timers
{
    public interface ITimer : IDisposable, IEquatable<ITimer>
    {
        event Action<ITimer> TimerTick;
        event Action<ITimer> TimerDone;

        int Id { get; }
        float Duration { get; }
        float Remaining { get; }
        float Elapsed { get; }
        float Progress { get; }
        float End { get; }

        void Tick();
        void Done();
    }
}