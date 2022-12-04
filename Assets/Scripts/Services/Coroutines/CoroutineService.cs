using System;
using System.Collections;
using UnityEngine;

namespace Services.Coroutines
{
    public class CoroutineService : MonoBehaviour
    {
        public static CoroutineService Instance;
        
        public Coroutine StartCoroutine(IEnumerator coroutine, Action callback)
        {
            return StartCoroutine(CallbackCoroutine(coroutine, callback));
        }
        
        public void DelayCall(Action action, float time, bool unscaledTime = false)
        {
            StartCoroutine(DelayCoroutine(action, time, unscaledTime));
        }

        public void Dispose()
        {
            StopAllCoroutines();
        }

        private IEnumerator CallbackCoroutine(IEnumerator coroutine, Action callback)
        {
            yield return coroutine;
            callback?.Invoke();
        } 

        private IEnumerator DelayCoroutine(Action action, float time, bool unscaledTime = false)
        {
            if (time > 0f)
            {
                if (unscaledTime)
                {
                    yield return new WaitForSecondsRealtime(time);
                }
                else
                {
                    yield return new WaitForSeconds(time);
                }
            }

            action?.Invoke();
        }

        private void Awake() => Instance = this;

        private void OnDestroy()
        {
            Dispose();
        }
    }
}