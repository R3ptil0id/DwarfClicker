using System.Collections.Generic;
using Controllers.GameController;
using UnityEngine;
using Utils.Ioc;

namespace Services.GameLoop
{
    [RegistrateMonoBehaviourInIoc]
    public class GameLoopService : MonoBehaviour
    {
        private readonly List<IUpdateListener> _updateListeners = new List<IUpdateListener>();

        public void Register(IUpdateListener listener)
        {
            _updateListeners.Add(listener);
        }

        public void Unregister(IUpdateListener listener)
        {
            _updateListeners.Remove(listener);
        }

        private void Update()
        {
            foreach (var listener in _updateListeners)
            {
                listener.Update(Time.deltaTime);
            }
        }
    }
}