using System;
using System.Collections.Generic;
using Enums;
using Futures.Base;
using Futures.Common;
using Futures.Util;
using UnityEngine;
using Utils;
using Utils.EnumExtensions;

namespace Controls.GameElements.CurrencyBar
{
    public class CurrencyBarControl : MonoBehaviour
    {
        [SerializeField]private float localYStartOffset;
        [SerializeField]private float localStartScale;
        [SerializeField]private float nextLevelLocalMiddleXScale;
        [SerializeField]private float nextLevelLocalFinishXScale;
        [SerializeField]private float nextLevelLocalYScale;
        [Space(2)]
        [SerializeField] private CurrencyType currencyType;
        [SerializeField] private CurrencyLevel currencyLevel;
        [Space(2)]
        [SerializeField] private List<Transform> _bars;
        [SerializeField] private Transform _nextBar;

        private Vector3 _startPosition;
        private int _index;
        private IFuture _future;
        
        public Action NotifyAnimationAddingBarComplete;
        
        public CurrencyType CurrencyType => currencyType;
        public CurrencyLevel CurrencyLevel => currencyLevel;
        
        public Guid Guid { get; private set; }
        public bool IsBusy { get; private set; }

        public void Release()
        {
            IsBusy = false;
            transform.position = _startPosition;
            
            InitView();
        }

        private void InitView()
        {
            foreach (var bar in _bars)
            {
                bar.gameObject.SetActive(false);
            }

            _nextBar.gameObject.SetActive(false);
        }

        public void Busy()
        {
            IsBusy = true;
        }

        public void Initialize(Vector3 startPosition)
        {
            IsBusy = false;
            Guid = Guid.NewGuid();
            _startPosition = startPosition;
            InitView();
        }
        
        public void AddLevel(int currentLvl, CurrencyLevel currencyLevel)
        {
            _index = currentLvl - 1;
            // UpdateView();
            TestUpdateLevel();
        }

        private void TestUpdateLevel() //TODO replace with animation
        {
            _bars[_index].gameObject.SetActive(true);
            if (_index + 1 < _bars.Count)
            {
                return;
            }

            foreach (var bar in _bars)
            {   
                bar.gameObject.SetActive(false);   
            }
            
            _nextBar.gameObject.SetActive(false);
        }

        private void UpdateView()
        {
            var barTransform = _bars[_index];
            var localPosition = barTransform.localPosition;
            var targetLocalPosition = localPosition;
            
            localPosition = new Vector3(targetLocalPosition.x, targetLocalPosition.y + localYStartOffset, targetLocalPosition.z);
            barTransform.localPosition = localPosition;
            
            var localScale = barTransform.localScale;
            barTransform.localScale = new Vector3(localStartScale, localScale.y, localScale.z);
            
            var scaleXFuture = FuturePool.Take<ScaleTransformFuture>().Initialize(barTransform,
                localScale, EasingFunction.Ease.EaseOutBack, 0.2f);
            
            var translatePositionFuture = FuturePool.Take<TranslateLocalPositionEasingFuture>()
                .Initialize(barTransform, localPosition, targetLocalPosition, EasingFunction.Ease.Linear, 0.1f);
            
            _future = new SequenceFuture();
            
            ((SequenceFuture)_future).AddFuture(scaleXFuture);
            ((SequenceFuture)_future).AddFuture(translatePositionFuture);
         
            barTransform.gameObject.SetActive(true);
            _future.AddListenerOnFinalize(OnFutureEnd);
            if (_index == _bars.Count - 1)
            {
                _future.AddListenerOnFinalize(OnFutureEnd);
            }
            
            _future.Run();
        }

        private void OnFutureEnd(IFuture future)
        {
            if (_index == _bars.Count - 1)
            {
                // _bar.gameObject.SetActive(true);
                _bars.ForEach(b => b.gameObject.SetActive(false));
            }
            NotifyAnimationAddingBarComplete?.Invoke();
            _future = null;
        }
    }
}