using System.Collections.Generic;
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

        private const float LocalYStartPositions = 0.076f;
        private const float LocalStartScale = 0.01f;
        
        private int _currentLvl = -1;
        private IFuture _future;
        
        private void Awake()
        {
            for (var i = 0; i < _barControls.Count; i++)
            {
                _barControls[i].Initialization(i);
            }
        }

        public void AddLevel()
        {
            _currentLvl++;

            UpdateView();
        }

        private void UpdateView()
        {
            var currentBar = _barControls[_currentLvl];
            var localPosition = currentBar.Bar.localPosition;
            var targetLocalPosition = localPosition;
            
            localPosition = new Vector3(targetLocalPosition.x, LocalYStartPositions, targetLocalPosition.z);
            currentBar.Bar.localPosition = localPosition;

            var localScale = currentBar.Bar.localScale;
            _barControls[_currentLvl].Bar.localScale = new Vector3(LocalStartScale, localScale.y, localScale.z);

            var scaleXFuture = FuturePool.Take<ScaleTransformFuture>().Initialize(_barControls[_currentLvl].Bar,
                localScale, EasingFunction.Ease.EaseOutBack, 0.4f);
            
            var translatePositionFuture = FuturePool.Take<TranslateLocalPositionEasingFuture>()
                .Initialize(_barControls[_currentLvl].Bar, localPosition, targetLocalPosition, EasingFunction.Ease.Linear, 0.2f);

            _future = new SequenceFuture();

            ((SequenceFuture)_future).AddFuture(scaleXFuture);
            ((SequenceFuture)_future).AddFuture(translatePositionFuture);

            _barControls[_currentLvl].Bar.gameObject.SetActive(true);

            if (_currentLvl == _barControls.Count - 1)
            {
                _future.AddListenerOnFinalize(OnFutureEnd);
            }

            _future.Run();
        }

        private void OnFutureEnd(IFuture future)
        {
            _bar.gameObject.SetActive(true);
            
            _barControls.ForEach(b => b.Bar.gameObject.SetActive(false));
            _future = null;
        }
    }
}