using System.Collections.Generic;
using System.Linq;
using Constants;
using Controllers.Perks;
using Controls;
using Enums;
using UnityEngine;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.DepositoryControllers
{
    public class CurrencyInDepositoryController : BaseController
    {
        [Inject] private readonly ObjectsInstaller _objectsInstaller;
        [Inject] private readonly PerksController _perksController;
        
        private readonly CurrencyObjectsPoolController _currencyObjectsPool;
        private readonly Dictionary<CurrencyType, List<CurrencyBarController>> _currentCurrencyBars = new();
        private readonly LinkedList<CurrencyBarController> _currencyBarControllers = new ();

        public CurrencyInDepositoryController()
        {
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {
                _currentCurrencyBars.Add(currencyType, new List<CurrencyBarController>());
            }

            _currencyObjectsPool = new CurrencyObjectsPoolController();
        }

        public bool TryAddCurrency(CurrencyType currencyType)
        {
            var bar = _currentCurrencyBars[currencyType].LastOrDefault(b => !b.Filled);
            
            if (bar == null)
            {
                return false;
            }

            bar.AddLevel();
            return true;
        }

        public void AddCurrencyBar(CurrencyType currencyType)
        {
            if (_currentCurrencyBars.TryGetValue(currencyType, out var barControllers) &&
                 barControllers.Count >= _perksController.GetPerksData<CurrencyBarPerks>().CurrentMaxCurrencyBars[currencyType])
            {
                return;
            }

            if (_currencyBarControllers.Count == 0)
            {
               var currencyBarController = CreateCurrencyBarController(currencyType, _objectsInstaller.Currencies.position);
               _currencyBarControllers.AddLast(currencyBarController);
                return;
            }

            var currentNode = _currencyBarControllers.Last;

            if (currentNode.Value.CurrencyType <= currencyType)
            {
                var currencyBarController= CreateCurrencyBarController(currencyType, currentNode.Value.GetPosition() + Vector3.right * DataConstants.PositionXOffsetCurrency);
                _currencyBarControllers.AddLast(currencyBarController);
                return;
            }

            while (currentNode != null)
            {
                if (currentNode.Value.CurrencyType <= currencyType)
                {
                    var currencyBarController= CreateCurrencyBarController(currencyType, currentNode.Value.GetPosition());
                    var node = _currencyBarControllers.AddAfter(currentNode, currencyBarController);
                    
                    MoveFollowing(node);
                    return;
                }

                if (currentNode.Previous == null)
                {
                    var currencyBarController= CreateCurrencyBarController(currencyType,
                        currentNode.Value.GetPosition()  + Vector3.left * DataConstants.PositionXOffsetCurrency);
                    var node = _currencyBarControllers.AddBefore(currentNode, currencyBarController);
                    MoveFollowing(node);
                    return;
                }

                currentNode = currentNode.Previous;
            }
        }

        private void MoveFollowing(LinkedListNode<CurrencyBarController> node)
        {
            var currentNode = node;

            while (currentNode != null)
            {
                var currencyBarController = currentNode.Value;
                currencyBarController.SetToPosition(currencyBarController.GetPosition() +
                                                    Vector3.right * DataConstants.PositionXOffsetCurrency);
                currentNode = currentNode.Next;
            }
        }

        private CurrencyBarController CreateCurrencyBarController(CurrencyType type, Vector3 position)
        {
            var control = _currencyObjectsPool.GetCurrencyObject(type);
            var currencyBarController= new CurrencyBarController(control, position);
            _currentCurrencyBars[type].Add(currencyBarController);   
            return currencyBarController;
        }
    }
}