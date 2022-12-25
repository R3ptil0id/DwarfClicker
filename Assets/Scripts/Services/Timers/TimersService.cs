using System;
using System.Collections.Generic;
using Services.GameLoop;
using Utils.Ioc;

namespace Services.Timers
{
    [RegistrateInIoc]
    public class TimersService : ITimersService
    {
        private static int _currentId = 0;

        private readonly Dictionary<int, LinkedListNode<ITimer>> _mapping =
            new Dictionary<int, LinkedListNode<ITimer>>();

        private readonly LinkedList<ITimer> _timers = new LinkedList<ITimer>();
        private readonly Dictionary<int, ITimer> _added = new Dictionary<int, ITimer>();
        private readonly Queue<LinkedListNode<ITimer>> _removed = new Queue<LinkedListNode<ITimer>>();
        
        private GameLoopService _gameLoopService;
        
        private double _serverTime;
        private double _serverStartTime;
        private DateTime _startTime;
        
        public event Action<ITimer> Added;
        public event Action<ITimer> Removed;
        
        public void Run()
        {
            _startTime = DateTime.UtcNow;
            _gameLoopService.Register(this);
        }

        public void Update(float deltaTime)
        {
            RemoveTimers();
            AddTimers();

            var node = _timers.Count > 0 ? _timers.First : null;

            while (node != null)
            {
                var timer = node.Value;

                if (timer.Remaining > 0d)
                {
                    timer.Tick();
                    node = node.Next;
                    continue;
                }

                timer.Done();
                _removed.Enqueue(node);
                _mapping.Remove(timer.Id);

                node = node.Next;
            }

            RemoveTimers();
        }

        public ITimer AddTimer(float duration, Action<ITimer> tick = null, Action<ITimer> done = null)
        {
            var id = GetId();
            var timer = CreateTimer(id, duration, tick, done);
            _added[timer.Id] = timer;
            return timer;
        }

        public void AddTimer(float duration, bool realtime, out ITimer timer)
        {
            var id = GetId();

            if (_mapping.TryGetValue(id, out var node))
            {
                timer = node.Value;
                return;
            }

            if (_added.TryGetValue(id, out timer))
            {
                return;
            }

            timer = CreateTimer(id, duration,  null, null);
            _added[timer.Id] = timer;
        }

        public bool TryAddTimer(ITimer timer)
        {
            if (_mapping.ContainsKey(timer.Id) || _added.ContainsKey(timer.Id))
            {
                return false;
            }

            _added[timer.Id] = timer;
            return true;
        }

        public void RemoveTimer(ITimer timerToRemove)
        {
            var id = timerToRemove.Id;

            if (_mapping.TryGetValue(id, out var timerNode))
            {
                if (timerNode.Value.Remaining > 0)
                {
                    timerNode.Value.Done();
                }

                _removed.Enqueue(timerNode);
                _mapping.Remove(id);
                return;
            }

            if (!_added.TryGetValue(id, out var timer))
            {
                return;
            }

            if (timer.Remaining > 0)
            {
                timer.Done();
            }

            _added.Remove(id);

            Removed?.Invoke(timer);
            timer.Dispose();
        }

        public void Dispose()
        {
            Removed = null;

            _gameLoopService.Unregister(this);

            while (_timers.Count > 0)
            {
                var node = _timers.First;
                node.Value.Dispose();
                _timers.Remove(node);
            }

            _timers.Clear();
            _mapping.Clear();
            _added.Clear();
            _removed.Clear();
        }

        public TimersService()
        {
            _gameLoopService = IoC.Resolve<GameLoopService>();
        }
        
        private void AddTimers()
        {
            foreach (var pair in _added)
            {
                var node = _timers.AddLast(pair.Value);
                _mapping[pair.Key] = node;

                Added?.Invoke(node.Value);
            }

            _added.Clear();
        }

        private void RemoveTimers()
        {
            while (_removed.Count > 0)
            {
                var node = _removed.Dequeue();

                Removed?.Invoke(node.Value);

                node.Value.Dispose();

                if (node.List == _timers)
                {
                    _timers.Remove(node);
                }
            }
        }

        private ITimer CreateTimer(int id, float duration,  Action<ITimer> tick, Action<ITimer> done)
        {
            return new Timer(id, duration, tick, done);
        }

        private static int GetId()
        {
            return _currentId++;
        }
    }
}