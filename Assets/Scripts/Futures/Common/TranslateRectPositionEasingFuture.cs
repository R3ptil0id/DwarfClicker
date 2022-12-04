using System;
using Futures.Base;
using Services.Timers;
using UnityEngine;
using Utils;

namespace Futures.Common
{
    public class TranslateRectPositionEasingFuture : TimerFuture
    {
        private RectTransform _rectTransform;
        private Vector2 _start;
        private Vector2 _end;
        private EasingFunction.Function _easingFunction;
        private Func<float, Vector2, Vector2, Vector2> _moveFunction;
        private float _time;
        
        public TranslateRectPositionEasingFuture Initialize(RectTransform rectTransform, Vector2 start, Vector2 end, EasingFunction.Ease ease, float time)
        {
            _rectTransform = rectTransform;
            _start = start;
            _end = end;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateRectPositionEasingFuture Initialize(RectTransform rectTransform, RectTransform end, EasingFunction.Ease ease, float time)
        {
            _rectTransform = rectTransform;
            _start = rectTransform.position;
            _end = end.position;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateRectPositionEasingFuture Initialize(RectTransform rectTransform, Vector3 offset, EasingFunction.Ease ease, float time)
        {
            _rectTransform = rectTransform;
            var position = rectTransform.position;
            _start = position;
            _end = position + offset;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _time = time;
            return this;
        }
        
        public TranslateRectPositionEasingFuture Initialize(RectTransform rectTransform, Vector2 start, Vector2 end, EasingFunction.Ease ease, Func<float, Vector2, Vector2, Vector2> moveFunction, float time)
        {
            _rectTransform = rectTransform;
            _start = start;
            _end = end;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _moveFunction = moveFunction;
            _time = time;
            return this;
        }
        
        public TranslateRectPositionEasingFuture Initialize(RectTransform rectTransform, RectTransform end, EasingFunction.Ease ease, Func<float, Vector2, Vector2, Vector2> moveFunction, float time)
        {
            _rectTransform = rectTransform;
            _start = rectTransform.position;
            _end = end.position;
            _easingFunction = EasingFunction.GetEasingFunction(ease);
            _moveFunction = moveFunction;
            _time = time;
            return this;
        }

        protected override void OnRun()
        {
            _rectTransform.position = _start;
            timer = timersService.AddTimer(_time, OnUpdate, (_) => Complete());
        }

        private void OnUpdate(ITimer _)
        {
            var progress = timer.Elapsed / _time;

            var t = _easingFunction(0f, 1f, progress);
            _rectTransform.position = 
                _moveFunction?.Invoke(t, _start, _end) 
                ?? Vector2.Lerp(_start, _end, t);
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            _rectTransform.position = _end;
        }
    }
}