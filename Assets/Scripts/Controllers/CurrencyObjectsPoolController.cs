using System.Collections.Generic;
using System.Linq;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

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
            
            for (var i = 0; i < _currenciesPrefabs.Count; i++)
            {
                var list = new List<CurrencyBarControl>();
                var currencyType = CurrencyType.Undefined;

                var type = _currenciesPrefabs.GetType();
                var fields = type.GetFields();
                
                foreach (var field in fields)
                {
                    var prefab = field.GetValue(_currenciesPrefabs) as GameObject;
                    
                    if (prefab == null)
                    {
                        return;
                    }
                    
                    for (var j = 0; j < InstancesCount; j++)
                    {
                        var instance = Object.Instantiate(prefab, _parentObject);
                        var component = instance.GetComponent<CurrencyBarControl>();

                        if (component.CurrencyType == CurrencyType.Undefined)
                        {
                            Debug.LogError($"component.CurrencyType: {component.CurrencyType}");
                            return;
                        }

                        currencyType = component.CurrencyType;
                        component.Initialize(_parentObject.position);
                        list.Add(component);
                    }
                }

                if (currencyType != CurrencyType.Undefined)
                {
                    _dictionary.Add(currencyType, list);
                }
            }
        }
    }
}