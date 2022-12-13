using System;
using System.Collections.Generic;
using Enums;
using Futures.Base;
using Futures.Common;
using Futures.Util;
using UnityEngine;
using Utils;

namespace Controls.GameElements.CurrencyBar
{
    public class ComplexCurrencyBarControl : CurrencyBarControl
    {
        [SerializeField] private List<SmallBarControl> _barControls;
        [SerializeField] private Transform _bar;

        private const float LocalYStartPositions = 0.98f;
        private const float LocalStartScale = 0.01f;
        
        private int _index;
        private IFuture _future;

        public Action NotifyAnimationComplete;
        
        private void Awake()
        {
            for (var i = 0; i < _barControls.Count; i++)
            {
                _barControls[i].Initialization(i);
            }
        }

        public void AddLevel(int currentLvl, CurrencyLevel currencyLevel = CurrencyLevel.Undefined)
        {
            if (currencyLevel == CurrencyLevel.Units_5)
            {
                _currencyLevel = currencyLevel;
            }

            _index = currentLvl - 1;

            UpdateView();
        }

        private void UpdateView()
        {
            var smallBarControl = _barControls[_index];
            var localPosition = smallBarControl.Bar.localPosition;
            var targetLocalPosition = localPosition;
            
            localPosition = new Vector3(targetLocalPosition.x, LocalYStartPositions, targetLocalPosition.z);
            smallBarControl.Bar.localPosition = localPosition;

            var localScale = smallBarControl.Bar.localScale;
            smallBarControl.Bar.localScale = new Vector3(LocalStartScale, localScale.y, localScale.z);

            var scaleXFuture = FuturePool.Take<ScaleTransformFuture>().Initialize(smallBarControl.Bar,
                localScale, EasingFunction.Ease.EaseOutBack, 0.2f);
            
            var translatePositionFuture = FuturePool.Take<TranslateLocalPositionEasingFuture>()
                .Initialize(smallBarControl.Bar, localPosition, targetLocalPosition, EasingFunction.Ease.Linear, 0.1f);

            _future = new SequenceFuture();

            ((SequenceFuture)_future).AddFuture(scaleXFuture);
            ((SequenceFuture)_future).AddFuture(translatePositionFuture);

            var small = smallBarControl.Bar;
            smallBarControl.Bar.gameObject.SetActive(true);

            if (_index == _barControls.Count - 1)
            {
                _future.AddListenerOnFinalize(OnFutureEnd);
            }

            _future.Run();
        }

        private void OnFutureEnd(IFuture future)
        {
            _bar.gameObject.SetActive(true);
            
            _barControls.ForEach(b => b.Bar.gameObject.SetActive(false));
            NotifyAnimationComplete?.Invoke();
            _future = null;
        }
    }
}