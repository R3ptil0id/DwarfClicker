using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controls.GameElements
{
    public class DepositoryControl : MonoBehaviour
    {
        [SerializeField] private Transform _startTransform;
        [SerializeField] private Dictionary<CurrencyType, float> _distanceBetween; 

        public Transform StartTransform => _startTransform;
    }
}