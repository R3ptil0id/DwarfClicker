using System;
using UnityEngine;

using UnityEngine.UI;

namespace Controls.UiControls.UiPerkButtonControls
{
    public abstract class UiBasePerkButtonControl : MonoBehaviour, IClickListener, IDisposable
    {
        private Button _button;

        public void Dispose()
        {
            if (_button == null)
                return;
            
            _button.onClick.RemoveAllListeners();
        }
        public void AddClickListener(Action action)
        {
            if (_button == null)
                _button = GetComponent<Button>();

            _button.onClick.AddListener(action.Invoke);
        }
    }
}