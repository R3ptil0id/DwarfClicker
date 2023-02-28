using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Controls.UiControls.UiPerkButtonControls
{
    public abstract class UiBasePerkButtonControl : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _button;

        public Action Clicked; 

        public void Dispose()
        {
            Clicked = null;

            if (_button == null)
                return;
            _button.clicked -= ClickedHandler;
        }

        private void Start()
        {
            if (_button == null)
                return;

            _button.clicked += ClickedHandler;
        }

        private void ClickedHandler()
        {
            Clicked?.Invoke();
        }
    }
}