using Enums;
using UnityEngine;

namespace Controls.GameElements.CurrencyBar
{
    public abstract class BaseCurrencyBarControl : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private CurrencyLevel _currencyLevel;
        
        public CurrencyType CurrencyType => _currencyType;
        public CurrencyLevel CurrencyLevel => _currencyLevel;
    }
}