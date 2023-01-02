using Constants;
using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class CurrencyBarController
    {
        protected readonly CurrencyBarControl _control;
        public CurrencyType CurrencyType { get; protected set;}
        public int Lvl { get; protected set;}
        public bool Filled => Lvl == DataConstants.CurrencyCountInType[CurrencyType];

        public CurrencyBarController(CurrencyBarControl control)
        {
            _control = control;
            
            CurrencyType = control.CurrencyType;
            AddLevel(1);
        }

        public void AddLevel(int lvl)
        {
            Lvl = lvl;
            _control.AddLevel(lvl);
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