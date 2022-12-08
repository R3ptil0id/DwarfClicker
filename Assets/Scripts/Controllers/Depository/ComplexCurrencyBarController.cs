using Constants;
using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class ComplexCurrencyBarController : CurrencyBarController
    {
        public int CurrentBarLvl { get; private set; }
        public ComplexCurrencyBarController(ComplexCurrencyBarControl control) : base(control)
        {
        }

        public void AppearAtPosition(Vector3 position)
        {
            _control.transform.position = position;
        }

        public void AddLevel(int lvl)
        {
            CurrentBarLvl += lvl;
            
            if(CurrentBarLvl == DataConstants.CurrencyValues[CurrencyLevel.Units_5])
            {
                _currencyLevel = CurrencyLevel.Units_5;
                _control.AddLevel(CurrentBarLvl, _currencyLevel);
            }
            else
            {
                _control.AddLevel(CurrentBarLvl);    
            }
        }        
    }
}