using System;
using Enums;
using TMPro;
using UnityEngine;
using Utils.Ioc;

namespace Controls.UiControls
{
    [RegistrateMonoBehaviourInIoc]
    public class UiInGameControl : MonoBehaviour
    {
        public RectTransform InGamePanel;
        
        [Space(6)]
        public TMP_Text Сurrency_0;
        public TMP_Text Сurrency_1;
        public TMP_Text Сurrency_2;
        public TMP_Text Сurrency_3;
        public TMP_Text Сurrency_4;

        public void UpdateInfo(CurrencyType type, float currencyValue)
        {
            var str = $"{(int)currencyValue:D10}"; //TODO real format
            switch (type)
            {
                case CurrencyType.Currency0:
                    Сurrency_0.text = str; 
                    break;
                case CurrencyType.Currency1:
                    Сurrency_1.text = str;
                    break;
                case CurrencyType.Currency2:
                    Сurrency_2.text = str;
                    break;
                case CurrencyType.Currency3:
                    Сurrency_3.text = str;
                    break;
                case CurrencyType.Currency4:
                    Сurrency_4.text = str;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}