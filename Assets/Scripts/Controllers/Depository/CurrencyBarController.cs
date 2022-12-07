using Controls.GameElements.CurrencyBar;
using Enums;

namespace Controllers.Depository
{
    public class CurrencyBarController
    {
        public CurrencyType _currencyType { get; protected set;}
        public CurrencyLevel _currencyLevel { get; protected set;}
        
        protected readonly ComplexCurrencyBarControl _control;

        public CurrencyBarController(ComplexCurrencyBarControl control)
        {
            _control = control;
            
            _currencyType = control.CurrencyType;
            _currencyLevel = control.CurrencyLevel;
        }
    }
}