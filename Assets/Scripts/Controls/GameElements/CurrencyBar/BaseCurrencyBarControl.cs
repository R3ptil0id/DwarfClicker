using Enums;
using UnityEngine;

namespace Controls.GameElements.CurrencyBar
{
    public abstract class BaseCurrencyBarControl : MonoBehaviour
    {
        [SerializeField] protected CurrencyType _currencyType;
        [SerializeField] protected CurrencyLevel _currencyLevel;
        
        public CurrencyType CurrencyType => _currencyType;
        public CurrencyLevel CurrencyLevel => _currencyLevel;
    }
}