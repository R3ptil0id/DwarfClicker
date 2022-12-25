using Futures.Base;
using Services.Timers;
using UnityEngine;
using Utils;
using Utils.EnumExtensions;

namespace Futures.Common
{
    public class ScaleTransformFuture : TimerFuture
    {
        private Transform _transform;
        private Vector3 _startScale;
        private Vector3 _endScale;
        private float _time;
        private EasingFunction.Function _func;

        public ScaleTransformFuture()
        {
            //no op
        }

        public ScaleTransformFuture Initialize(Transform transform, Vector3 startScale, Vector3 endScale,
            EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _startScale = startScale;
            _endScale = endScale;
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(Transform transform, Vector3 endScale, EasingFunction.Ease ease,
            float time)
        {
            _transform = transform;
            _startScale = _transform.localScale;
            _endScale = endScale;
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(RectTransform transform, Vector2 startScale, Vector2 endScale,
            EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _startScale = new Vector3(startScale.x, startScale.y, 1);
            _endScale = new Vector3(endScale.x, endScale.y, 1);
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(RectTransform transform, Vector2 endScale, EasingFunction.Ease ease,
            float time)
        {
            var localScale = transform.localScale;
            _transform = transform;
            _startScale = localScale;
            _endScale = new Vector3(endScale.x, endScale.y, 1);
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(Transform transform, float startScale, float endScale,
            EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _startScale = startScale * Vector3.one;
            _endScale = endScale * Vector3.one;
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(RectTransform transform, float startScale, float endScale,
            EasingFunction.Ease ease, float time)
        {
            _transform = transform;
            _startScale = startScale * Vector2.one;
            _endScale = endScale * Vector2.one;
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        public ScaleTransformFuture Initialize(Transform transform, float endScale, EasingFunction.Ease ease,
            float time)
        {
            _transform = transform;
            _startScale = _transform.localScale;
            _endScale = endScale * Vector2.one;
            _time = time;
            _func = EasingFunction.GetEasingFunction(ease);
            return this;
        }

        protected override void OnRun()
        {
            _transform.localScale = _startScale;
            timer = _timersService.AddTimer(_time, OnUpdate, (_) => Complete());
        }

        private void OnUpdate(ITimer _)
        {
            var progress = timer.Elapsed / _time;
            _transform.localScale = new Vector3(
                _func(_startScale.x, _endScale.x, progress),
                _func(_startScale.y, _endScale.y, progress),
                _func(_startScale.z, _endScale.z, progress));
        }

        protected override void OnComplete()
        {
            base.OnComplete();
            _transform.localScale = _endScale;
        }
    }
}