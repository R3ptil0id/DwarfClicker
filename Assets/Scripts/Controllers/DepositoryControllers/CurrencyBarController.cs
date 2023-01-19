using Constants;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    public class CurrencyBarController : BaseController
    {
        [Inject] private readonly ObjectsInstaller _objectsInstaller;
        private readonly CurrencyBarControl _control;
        public CurrencyType CurrencyType { get; protected set;}
        public int Lvl { get; protected set;}
        public bool Filled => Lvl == DataConstants.CurrencyCountInType[CurrencyType];

        public CurrencyBarController(CurrencyBarControl control, Vector3 position)
        {
            _control = control;
            Initialize(control, position);
        }

        private void Initialize(CurrencyBarControl control, Vector3 position)
        {
            _control.transform.SetParent(_objectsInstaller.Currencies);
            _control.Initialize(position);
            CurrencyType = control.CurrencyType;
        }

        public void AddLevel(int lvl)
        {
            Lvl = lvl;
            _control.AddLevel(lvl);
        }

        public Vector3 GetPosition()
        {
            return _control.transform.position;
        }

        public void SetToPosition(Vector3 position)
        {
            _control.transform.position = position;
        }
    }
}