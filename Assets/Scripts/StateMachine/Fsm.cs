using System;
using System.Collections.Generic;

namespace LocalMultiplayer.StateMachine
{
    public class Fsm : IUpdateListener
    {
        private IFsmState _currentFsmState;
        
        private readonly Dictionary<string, object> _blackboard = new Dictionary<string, object>();
        private readonly Dictionary<Type, IFsmState> _states = new Dictionary<Type, IFsmState>();

        public IFsmState CurrentFsmState => _currentFsmState;

        public void Initialize(IConfig config, List<IFsmState> states)
        {
            foreach (var state in states)        
            {
                state.Initialize(this, config);
                _states.Add(state.GetType(), state);
            }
        }

        public void Update()
        {
            _currentFsmState?.Update();
        }
        
        public void GoToState<T>()
        {
            _currentFsmState?.Exit();
            
            _states.TryGetValue(typeof(T), out _currentFsmState);
            
            _currentFsmState?.Enter();
        }
        
        public T GetBlackboardValue<T>(string name) {
            _blackboard.TryGetValue(name, out var result);
            return (T)result;
        }
        
        public object GetBlackboardValue(string name)
        {
            _blackboard.TryGetValue(name, out var result);
            return result;
        }

        public void SetBlackboardValue(string name, object value)
        {
            _blackboard[name] = value;
        }
    }
}