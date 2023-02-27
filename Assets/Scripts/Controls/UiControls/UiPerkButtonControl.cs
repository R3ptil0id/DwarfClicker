using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Controls.UiControls
{
    public class UiPerkButtonControl : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _button;
        
        public TMP_Text TypePriceText;
        public TMP_Text PriceText;
        public Action Clicked;

        void Start()
        {
            if (_button == null)
                return;
            
            _button.clicked += ClickedHandler;
        }

        private void ClickedHandler()
        {
            Clicked?.Invoke();
        }

        public void Dispose()
        {
            Clicked = null;
            
            if (_button == null)
                return;
            _button.clicked -= ClickedHandler;
        }
    }
}