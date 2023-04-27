using System;
using TMPro;
using UnityEngine.UI;

namespace Controls.UiControls.UiPerkButtonControls
{
    public class UiBuyPerkButtonControl : UiBasePerkButtonControl, IClickListener
    {
        private Button _button;
        
        public TMP_Text TypePriceText;
        public TMP_Text PriceText;
        
        public override void Dispose()
        {
            base.Dispose();
            
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