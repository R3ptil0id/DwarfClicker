using System;
using UnityEngine;

using UnityEngine.UI;

namespace Controls.UiControls.UiPerkButtonControls
{
    public abstract class UiBasePerkButtonControl : MonoBehaviour, IDisposable
    {
        private Button _button;

        public Action Clicked; 

        public void Dispose()
        {
            Clicked = null;

            if (_button == null)
                return;
            
            _button.onClick.RemoveListener(ClickedHandler);
        }

        private void Start()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            _button.onClick.AddListener(ClickedHandler);
        }

        private void ClickedHandler()
        {
            Clicked?.Invoke();
        }
    }
}