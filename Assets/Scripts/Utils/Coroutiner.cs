using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Ioc;

namespace Utils
{
    [RegistrateMonoBehaviourInIoc()]
    public class Coroutiner : MonoBehaviour
    {
        private Dictionary<Guid, Func<IEnumerator>> _coroutines = new(); 
        
        public Guid CoroutineRun(Func<IEnumerator> action)
        {
            var guid = Guid.NewGuid();
            StartCoroutine(action());
            
            return guid;
        }
        
        public void StopCoroutine(Guid guid)
        {
            if (_coroutines.TryGetValue(guid, out var coroutine))
            {
                StopCoroutine(coroutine());    
            }
        }

        public void StopCoroutines()
        {
            StopAllCoroutines();
        }
    }
}