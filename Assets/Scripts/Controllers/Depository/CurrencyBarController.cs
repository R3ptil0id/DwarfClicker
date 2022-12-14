using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class CurrencyBarController
    {
        public CurrencyType CurrencyType { get; protected set;}
        public CurrencyLevel CurrencyLevel { get; protected set;}
        
        protected readonly CurrencyBarControl _control;

        public CurrencyBarController(CurrencyBarControl control)
        {
            _control = control;
            
            CurrencyType = control.CurrencyType;
            CurrencyLevel = control.CurrencyLevel;
        }

        public Vector3 GetLocalPosition()
        {
            return _control.transform.localPosition;
        }

        public void SetToPosition(Vector3 position)
        {
            _control.transform.localPosition = position;
        }
    }
}