using System.Collections.Generic;
using Controllers.GameController;
using UnityEngine;

namespace Services.GameLoop
{
    public class GameLoopService : MonoBehaviour
    {
        public static GameLoopService Instance;
        
        private readonly List<IUpdateListener> _updateListeners = new List<IUpdateListener>();

        public void Register(IUpdateListener listener)
        {
            _updateListeners.Add(listener);
        }

        public void Unregister(IUpdateListener listener)
        {
            _updateListeners.Remove(listener);
        }

        private void Awake() => Instance = this;

        private void Update()
        {
            foreach (var listener in _updateListeners)
            {
                listener.Update(Time.deltaTime);
            }
        }
    }
}