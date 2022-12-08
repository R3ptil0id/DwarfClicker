using Constants;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class CurrencyDepositoryBlock
    {   
        private const int MaxCountInBlock = 4;
        private int _count;
        public CurrencyType CurrencyType { get; private set; }
        public CurrencyLevel CurrencyLevel { get; private set; }
        
        public Vector3 Positon { get; private set; }
        
        public int Cost => DataConstants.CurrencyValues[CurrencyLevel] * _count;

        public bool IsFull
        {
            get
            {
                return _count == MaxCountInBlock;
            }
            
            private set { }
        }

        public int AddBar(CurrencyType type, CurrencyLevel level, Vector3 position)
        {
            if (CurrencyType != type || CurrencyLevel != level)
            {
                return -1;
            }
            
            var currentCost = Cost;
            if (_count == 0)
            {
                CurrencyType = type;
                CurrencyLevel = level;
                _count++;
                
                return Cost;
            }

            Positon = position;
            _count++;
            return Cost-currentCost;
        }
    }
}