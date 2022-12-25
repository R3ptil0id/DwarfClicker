using System;
using Futures.Base;
using Services.Timers;
using UnityEngine;
using Utils;
using Utils.EnumExtensions;

namespace Futures.Common
{
    public class TranslateLocalPositionEasingFuture : TimerFuture
    {
        private Transform _transform;
        private Vector2 _start;
        private Vector2 _end;
        private EasingFunction.Function _easingFunction;
        private Func<float, Vector2, Vector2, Vector2> _moveFunction;
        private float _time;
        
        public TranslateLocalPositionEasingFuture Initialize(Transform transform, Vector2 start, Vector2 end, EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _start = start;
            _end = end;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateLocalPositionEasingFuture Initialize(Transform transform, Transform end, EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _start = transform.localPosition;
            _end = end.localPosition;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateLocalPositionEasingFuture Initialize(Transform transform, Vector3 offset, EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            var localPosition = transform.localPosition;
            _start = localPosition;
            _end = localPosition + offset;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateLocalPositionEasingFuture Initialize(Transform transform, Vector2 start, Vector2 end, EasingFunction.Ease ease, Func<float, Vector2, Vector2, Vector2> moveFunction, float time)
        {
            _transform = transform;
            _start = start;
            _end = end;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _moveFunction = moveFunction;
            _time = time;
            return this;
        }
        
        public TranslateLocalPositionEasingFuture Initialize(Transform transform, Transform end, EasingFunction.Ease ease, Func<float, Vector2, Vector2, Vector2> moveFunction, float time)
        {
            _transform = transform;
            _start = transform.localPosition;
            _end = end.localPosition;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _moveFunction = moveFunction;
            _time = time;
            return this;
        }

        protected override void OnRun()
        {
            _transform.localPosition = _start;
            timer = _timersService.AddTimer(_time, OnUpdate, (_) => Complete());
        }

        private void OnUpdate(ITimer _)
        {
            var progress = timer.Elapsed / _time;

            var t = _easingFunction(0f, 1f, progress);
            _transform.localPosition = 
                _moveFunction?.Invoke(t, _start, _end) 
                ?? Vector2.Lerp(_start, _end, t);
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            _transform.localPosition = _end;
        }
    }
}