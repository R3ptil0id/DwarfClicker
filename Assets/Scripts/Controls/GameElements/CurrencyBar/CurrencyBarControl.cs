
using System;
using UnityEngine;

namespace Controls.GameElements.CurrencyBar
{
    public class CurrencyBarControl : BaseCurrencyBarControl
    {
        public Guid Guid { get; private set; }
        public bool IsBusy { get; private set; }

        private Vector3 _startPosition;

        public void Release()
        {
            IsBusy = false;
            transform.position = _startPosition;
        }
        
        public void Busy()
        {
            IsBusy = true;
        }


        public void Initialzie(Vector3 startPosition)
        {
            Guid = new Guid();
            _startPosition = startPosition;
        }
    }
}