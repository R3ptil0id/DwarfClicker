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
        [Inject] private ObjectsInstaller _objectsInstaller;
        
        private readonly CurrencyBarControl _control;
        private int _lvl;
        public CurrencyType CurrencyType { get; protected set;}
        public bool Filled => _lvl == CommonConstants.CurrencyCountInType[CurrencyType];

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

        public void AddLevel()
        {
            _lvl++;
            _control.AddLevel();
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