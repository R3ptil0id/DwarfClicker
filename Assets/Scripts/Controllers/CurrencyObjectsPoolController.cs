using System;
using System.Collections.Generic;
using System.Linq;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;
using Object = UnityEngine.Object;

namespace Controllers
{
    [RegistrateInIoc(needInitialize: true)]
    public class CurrencyObjectsPoolController : BaseController, IInitializable
    {
        [Inject] private readonly CurrenciesElementsPrefabs _currenciesPrefabs;
        [Inject] private readonly ObjectsInstaller _objectsInstaller;
        
        private Transform _parentObject;
        private Dictionary<CurrencyType, List<CurrencyBarControl>> _dictionary;
        
        private const int InstancesCount = 20;
        
        
        public CurrencyBarControl GetCurrencyObject(CurrencyType type)
        { 
            var control = _dictionary[type].FirstOrDefault(c => !c.IsBusy);
            if (control != null)
            {
                control.Busy();
            }

            return control;
        }
        
        public void Release(CurrencyBarControl control)
        {
            control.Release();
        }
        
        public void Initialize()
        {
            _parentObject = _objectsInstaller.PoolObject;
            _dictionary = new Dictionary<CurrencyType, List<CurrencyBarControl>>(_currenciesPrefabs.Count);
             var type = _currenciesPrefabs.GetType();
             var fields = type.GetFields();

                foreach (var field in fields)
                {
                    var prefab = field.GetValue(_currenciesPrefabs) as GameObject;
                    var currencyType = CurrencyType.Undefined;
                    var list = new List<CurrencyBarControl>();

                    if (prefab == null)
                    {
                        break;
                    }

                    for (var j = 0; j < InstancesCount; j++)
                    {
                        var instance = Object.Instantiate(prefab, _parentObject);
                        var component = instance.GetComponent<CurrencyBarControl>();

                        if (component.CurrencyType == CurrencyType.Undefined)
                        {
                            Debug.LogError($"{prefab} has component.CurrencyType: {component.CurrencyType}");
                            continue;
                        }

                        currencyType = component.CurrencyType;
                        component.Initialize(_parentObject.position);
                        list.Add(component);
                    }

                    if (currencyType != CurrencyType.Undefined)
                    {
                        if (_dictionary.ContainsKey(currencyType))
                        {
                            throw new ArgumentException($"{currencyType} already added to Currencies pool");
                        }

                        _dictionary.Add(currencyType, list);
                    }
                }
        }
    }
}