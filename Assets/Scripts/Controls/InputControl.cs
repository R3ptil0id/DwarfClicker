using System;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc(needInitialize: true)]
    public class InputControl : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _zoomFactor;
        [SerializeField] private float _zoomLerpSpeed;

        private Camera _camera;
        private float _targetZoom;
        public Action<CurrencyType> NotifyClickAddCurrency;
        public Action<CurrencyType> NotifyClickAddBar;
        public Action NotifyClickAddBot;

        public void ClickAddCurrency0()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_0);
        }
        
        public void ClickAddCurrency1()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_1);
        }
        
        public void ClickAddCurrency2()
        {
            NotifyClickAddCurrency?.Invoke(CurrencyType.Currency_2);
        }
        
        public void Initialize()
        {
            var cameraControl = IoC.Resolve<CameraControl>();
            _camera = cameraControl.Camera;
        }
        
        public void ClickAddCurrencyBar0()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_1);
        }
        
        public void ClickAddCurrencyBar1()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_2);
        }
        
        public void ClickAddCurrencyBar2()
        {
            NotifyClickAddBar?.Invoke(CurrencyType.Currency_2);
        }
        
        public void ClickAddBot()
        {
            NotifyClickAddBot?.Invoke();
        }
    }
}