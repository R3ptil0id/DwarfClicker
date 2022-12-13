using System;
using System.Collections.Generic;
using Constants;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class CurrencyDepositoryBlock
    {   
        private const int MaxCountInBlock = 4;
        private List<CurrencyBarController> _controllers = new List<CurrencyBarController>(MaxCountInBlock);
        private Vector3 _nextPosition;
        private ComplexCurrencyBarController _complexCurrency;
        public CurrencyType CurrencyType { get; private set; }
        public CurrencyLevel CurrencyLevel { get; private set; }
        public int Cost => DataConstants.CurrencyValues[CurrencyLevel] * _controllers.Count;
        public bool IsFull
        {
            get => _controllers.Count == MaxCountInBlock;
            private set { }
        }

        public CurrencyDepositoryBlock(Vector3 startPosition)
        {
            _nextPosition = startPosition;
        }

        public bool AddBar(CurrencyBarController currentBarController)
        {
            _complexCurrency = currentBarController as ComplexCurrencyBarController;
            
            if (_controllers.Count == 0)
            {
                CurrencyType = currentBarController.CurrencyType;
                CurrencyLevel = currentBarController.CurrencyLevel;
            }
            
            if (CurrencyType != currentBarController.CurrencyType ||
                CurrencyLevel != currentBarController.CurrencyLevel)
            {
                throw new ArgumentException(
                    $"CurrencyType: {currentBarController.CurrencyType}," +
                    $" CurrencyLevel: {currentBarController.CurrencyLevel}"
                );
            }
            
            _controllers.Add(currentBarController);
            currentBarController.SetToPosition(_nextPosition);
            
            var levelForSize = currentBarController.CurrencyLevel == CurrencyLevel.Units_1
                ? CurrencyLevel.Units_5
                : currentBarController.CurrencyLevel; 
            
            var size = DataConstants.Sizes[levelForSize];
            
            switch (_controllers.Count)
            {
                case 1:
                    _nextPosition += Vector3.right * DataConstants.PositionOffsetCurrency + Vector3.right * size;
                    break;
                case 2:
                    _nextPosition -= Vector3.right * DataConstants.PositionOffsetCurrency - Vector3.right * size;
                    _nextPosition += Vector3.up * DataConstants.PositionOffsetCurrency  + Vector3.up * size;
                    break;
                case 3:
                    _nextPosition += Vector3.right * DataConstants.PositionOffsetCurrency + Vector3.right * size;
                    break;
            }
            
            return IsFull;
        }
        public Vector3 GetNextPosition()
        {
            var currencyBarController = _controllers[2];
            var size = DataConstants.Sizes[currencyBarController.CurrencyLevel];
            return currencyBarController.GetLocalPosition() + Vector3.right * size + Vector3.right * DataConstants.PositionOffsetCurrency;
        }
    }
}