using System.Collections.Generic;
using Constants;
using Controls;
using Enums;
using UnityEngine;
using Utils;

namespace Controllers.Depository
{
    public class AdderCurrencyToDepository
    {
        private readonly Dictionary<CurrencyType, int> _currencyValues = new();
        private readonly LinkedList<CurrencyDepositoryBlock> BlocksInDepository = new();
        
        private readonly Installer _installer;
        private readonly PerkController _perkController;
        private readonly CurrencyObjectsPool _currencyObjectsPool;
        
        private bool _autoConvert;
        
        public Dictionary<CurrencyType, int> CurrencyValues => _currencyValues;
        
        public AdderCurrencyToDepository(Installer installer)
        {
            _installer = installer;
            _perkController = installer.GetInstance<PerkController>();
            _currencyObjectsPool = installer.GetInstance<CurrencyObjectsPool>();
            
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currencyValues.Add(currencyType, 0);
            }
        }

        public void AddCurrency(CurrencyType type, CurrencyLevel level)
        {
            var index = -1;
            var lastBlock = BlocksInDepository.Last;
            while (lastBlock?.Value != null)
            {
                if (lastBlock.Value.CurrencyType != type || lastBlock.Value.CurrencyLevel != level || lastBlock.Value.IsFull)
                {
                    lastBlock = lastBlock.Previous;
                }
                else
                {
                    break;
                }
            }

            Vector3 nextPosition;

            if (lastBlock?.Value != null)
            {
                nextPosition = lastBlock.Value.GetNextPosition();
            }
            else
            {
                var offset = DataConstants.Sizes[CurrencyLevel.Units_5] / 2;
                nextPosition = Vector3.right * offset + Vector3.up * offset;
            }
            
            var control = _currencyObjectsPool.GetCurrencyObject(type, level);
            control.transform.SetParent(_installer.Currencies);
            var controller = new CurrencyBarController(control);
            
            var currentDepositoryBlock = lastBlock?.Value ?? new CurrencyDepositoryBlock(nextPosition);
            currentDepositoryBlock.AddBar(controller);

            if (currentDepositoryBlock.IsFull)
            {
                
            }
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