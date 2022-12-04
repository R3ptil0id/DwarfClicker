using System;
using UnityEngine;

namespace Services.Timers
{
    public sealed class Timer : ITimer, IDisposable
    {
        private float _remainingTime;
        private int _frame = -1;

        public event Action<ITimer> TimerTick;
        public event Action<ITimer> TimerDone;

        public int Id { get; }
        public float Duration { get; }
        public float Progress => 1f - Remaining / Duration;
        public float End { get; }
        public float Elapsed => Duration - Remaining;

        public float Remaining
        {
            get
            {
                if (_frame == Time.frameCount)
                {
                    return _remainingTime > 0f ? _remainingTime : 0f;
                }

                _frame = Time.frameCount;
                _remainingTime = End - Time.time;


                return _remainingTime > 0f ? _remainingTime : 0f;
            }
        }

        public Timer(int id, float duration, Action<ITimer> tick, Action<ITimer> done)
        {
            Id = id;

            End = Time.time + duration;


            Duration = duration;
            TimerTick = tick;
            TimerDone = done;
        }

        public void Tick()
        {
            TimerTick?.Invoke(this);
        }
        
        public void Done()
        {
            TimerDone?.Invoke(this);
        }
        
        public void Dispose()
        {
            TimerTick = null;
            TimerDone = null;
        }

        public bool Equals(ITimer other)
        {
            return ReferenceEquals(this, other)
                   || other != null
                   && Duration.Equals(other.Duration)
                   && Id == other.Id;
        }
    }
}