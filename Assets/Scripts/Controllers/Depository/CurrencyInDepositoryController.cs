using System.Collections.Generic;
using System.Linq;
using Controls;
using Enums;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.Depository
{
    public class CurrencyInDepositoryController
    {
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        private readonly Installer _installer;
        private readonly LinkedList<CurrencyBarController> _currencyBarControllers = new ();
        
        public Dictionary<CurrencyType, int> CurrencyValues => _currencyValues;
        
        public CurrencyInDepositoryController()
        {
            _installer = IoC.Resolve<Installer>();
            
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
        }

        public void AddCurrency(CurrencyType type)
        {
            // var bar = _currencyBarControllers.LastOrDefault(c => c.CurrencyType == type);
            //
            // if (bar == null)
            // {
            //     var parent = _installer.Currencies;
            //     var 
            //     bar = new CurrencyBarController();
            // }
            // else
            // {
            //     
            // }
            
        }

        // public void AddAllCurrency(CurrencyType type, CurrencyLevel level)
        // {
        //     if (type == CurrencyType.Currency_0 && level == CurrencyLevel.Units_1)
        //     {
        //         if (_currentBarController == null)
        //         {
        //             var control =
        //                 (ComplexCurrencyBarControl)_currencyObjectsPool.GetCurrencyObject(CurrencyType.Currency_0,
        //                     CurrencyLevel.Units_1);
        //
        //             control.transform.SetParent(_installer.Currencies);
        //             _currentBarController = new ComplexCurrencyBarController(control);
        //             _currentBarController.SetToPosition(_currentPosition);
        //         }
        //
        //        _currentBarController.AddLevel(DataConstants.CurrencyValues[CurrencyLevel.Units_1]);
        //
        //         if (_currentBarController.CurrencyLevel != CurrencyLevel.Units_5)
        //         {
        //             return;
        //         }
        //         
        //         _currentBarController.AddListener(delegate
        //         {
        //             var lastBlock = BlocksInDepository.Last;
        //             while (lastBlock != null)
        //             {
        //                 if (lastBlock.Value.CurrencyType != CurrencyType.Currency_0)
        //                 {
        //                     lastBlock = lastBlock.Previous;
        //                 }
        //                 else
        //                 {
        //                     break;
        //                 }
        //             }
        //
        //             if (lastBlock?.Value == null && _currentBarController.CurrentBarLvl ==
        //                 DataConstants.CurrencyValues[CurrencyLevel.Units_5])
        //             {
        //                 var depositoryBlock = new CurrencyDepositoryBlock(_currentBarController.Position);
        //                 depositoryBlock.AddBar(_currentBarController);
        //                 BlocksInDepository.AddLast(depositoryBlock);
        //             }
        //             else
        //             {
        //                 lastBlock?.Value?.AddBar(_currentBarController);
        //             }
        //
        //             _currentBarController = null;
        //             _currentPosition += Vector3.right * DataConstants.Sizes[CurrencyLevel.Units_5] +
        //                                 Vector3.right * DataConstants.PositionOffsetCurrency;
        //         });
        //     }
        //
        //     var currencyDepositoryBlock = BlocksInDepository.Last;
        //     if (currencyDepositoryBlock?.Value == null)
        //     {
        //         return;
        //     }
        //     
        //     while ((currencyDepositoryBlock.Value.CurrencyType != type &&
        //             currencyDepositoryBlock.Value.CurrencyLevel != level) ||
        //            currencyDepositoryBlock.Previous != null)
        //     {
        //         currencyDepositoryBlock = currencyDepositoryBlock.Previous;
        //     }
        // }

        // private void AddToBlock
    }
}