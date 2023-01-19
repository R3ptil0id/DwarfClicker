using System;
using Enums;
using Futures.Base;
using UnityEngine;

namespace Controls.GameElements.CurrencyBar
{
    public class CurrencyBarControl : MonoBehaviour
    {
        [Space(2)]
        [SerializeField] private CurrencyType _currencyType;
        [Space(2)]
        [SerializeField] private Transform _bar;
        [SerializeField] private Transform _fill;

        private Vector3 _startPosition;
        private IFuture _future;
        
        public Action NotifyAnimationAddingBarComplete;
        
        public CurrencyType CurrencyType => _currencyType;
        
        public Guid Guid { get; private set; }
        public bool IsBusy { get; private set; }

        public void Release()
        {
            IsBusy = false;
            
            InitView();
        }

        private void InitView()
        {
            _bar.gameObject.SetActive(false);
            _fill.localScale = new Vector3(1, 0, 1);
        }

        public void Busy()
        {
            IsBusy = true;
        }

        public void Initialize(Vector3 startPosition)
        {
            Guid = Guid.NewGuid();
            transform.position = startPosition;
            // _startPosition = startPosition;
            InitView();
        }
        
        public void AddLevel()
        {
            // UpdateView();
            TestUpdateLevel();
        }

        private void TestUpdateLevel() //TODO replace with animation
        {
            _fill.localScale += new Vector3(0, 0.1f, 0);
        }

        private void UpdateView()
        {
            // var barTransform = _bars[_index];
            // var localPosition = barTransform.localPosition;
            // var targetLocalPosition = localPosition;
            //
            // localPosition = new Vector3(targetLocalPosition.x, targetLocalPosition.y + localYStartOffset, targetLocalPosition.z);
            // barTransform.localPosition = localPosition;
            //
            // var localScale = barTransform.localScale;
            // barTransform.localScale = new Vector3(localStartScale, localScale.y, localScale.z);
            //
            // var scaleXFuture = FuturePool.Take<ScaleTransformFuture>().Initialize(barTransform,
            //     localScale, EasingFunction.Ease.EaseOutBack, 0.2f);
            //
            // var translatePositionFuture = FuturePool.Take<TranslateLocalPositionEasingFuture>()
            //     .Initialize(barTransform, localPosition, targetLocalPosition, EasingFunction.Ease.Linear, 0.1f);
            //
            // _future = new SequenceFuture();
            //
            // ((SequenceFuture)_future).AddFuture(scaleXFuture);
            // ((SequenceFuture)_future).AddFuture(translatePositionFuture);
            //
            // barTransform.gameObject.SetActive(true);
            // _future.AddListenerOnFinalize(OnFutureEnd);
            // if (_index == _bars.Count - 1)
            // {
            //     _future.AddListenerOnFinalize(OnFutureEnd);
            // }
            //
            // _future.Run();
        }

        private void OnFutureEnd(IFuture future)
        {
            // if (_index == _bars.Count - 1)
            // {
            //     // _bar.gameObject.SetActive(true);
            //     _bars.ForEach(b => b.gameObject.SetActive(false));
            // }
            NotifyAnimationAddingBarComplete?.Invoke();
            _future = null;
        }
    }
}