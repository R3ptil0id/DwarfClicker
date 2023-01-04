using System;
using Enums;
using TMPro;
using UnityEngine;
using Utils.Ioc;

namespace Controls.UiControls
{
    [RegistrateMonoBehaviourInIoc]
    public class CurrenciesUiControl : MonoBehaviour
    {
        public TMP_Text Сurrency_0;
        public TMP_Text Сurrency_1;
        public TMP_Text Сurrency_2;
        public TMP_Text Сurrency_3;
        public TMP_Text Сurrency_4;

        public void UpdateInfo(CurrencyType type, int currencyValue)
        {
            var str = $"{currencyValue:D10}";
            switch (type)
            {
                case CurrencyType.Currency_0:
                    Сurrency_0.text = str; 
                    break;
                case CurrencyType.Currency_1:
                    Сurrency_1.text = str;
                    break;
                case CurrencyType.Currency_2:
                    Сurrency_2.text = str;
                    break;
                case CurrencyType.Currency_3:
                    Сurrency_3.text = str;
                    break;
                case CurrencyType.Currency_4:
                    Сurrency_4.text = str;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}