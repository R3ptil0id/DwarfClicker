using System.Collections.Generic;
using Controls;
using Controls.GameElements.CurrencyBar;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

namespace Controllers
{
    [RegistrateInIoc(needInitialize: true)]
    public class CurrencyObjectsPool : IInitializable
    {
        private readonly CurrenciesElementsPrefabs _currenciesPrefabs;
        private readonly Transform _parentObject;
        
        private readonly Dictionary<CurrencyType, Dictionary<CurrencyType, List<CurrencyBarControl>>> _dictionary;
        
        public CurrencyObjectsPool()
        {
            _parentObject = IoC.Resolve<Installer>().PoolObject;
            _currenciesPrefabs = IoC.Resolve<CurrenciesElementsPrefabs>();
            // _dictionary = new Dictionary<CurrencyType, Dictionary<CurrencyLevel, List<CurrencyBarControl>>>();
        }
        //
        // public CurrencyBarControl GetCurrencyObject(CurrencyType type, CurrencyLevel level)
        // {   
        //     foreach (var control in _dictionary[type][level].Where(control => !control.IsBusy))
        //     {
        //         control.Busy();
        //         return control;
        //     }
        //
        //     return null;
        // }
        //
        // public void Release(CurrencyBarControl control)
        // {
        //     control.Release();
        // }
        //
        public void Initialize()
        {
        //     foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
        //     {   
        //         var dict = new Dictionary<CurrencyLevel, List<CurrencyBarControl>>();
        //         foreach (var currencyLevel in EnumExtension.GetAllItems<CurrencyLevel>())
        //         {
        //             var list = new List<CurrencyBarControl>();
        //             dict.Add(currencyLevel, list);
        //         }   
        //         
        //         _dictionary.Add(currencyType, dict);
        //     }
        //     
        //     for (var i = 0; i < 20; i++)
        //     {
        //         var type = _currenciesPrefabs.GetType();
        //         var fields = type.GetFields();
        //
        //         foreach (var field in fields)
        //         {
        //             var prefab = (GameObject)field.GetValue(_currenciesPrefabs);
        //             var instance = Object.Instantiate(prefab, _parentObject);
        //             var component = instance.GetComponent<CurrencyBarControl>();
        //             
        //             if (component.CurrencyType == CurrencyType.Undefined ||
        //                 component.CurrencyLevel == CurrencyLevel.Undefined)
        //             {
        //                 Debug.LogError($"component.CurrencyType: {component.CurrencyType} / component.CurrencyLevel {component.CurrencyLevel} in {this}");
        //                 return;
        //             }
        //
        //             component.Initialize(_parentObject.position);
        //             _dictionary[component.CurrencyType][component.CurrencyLevel].Add(component);
        //         }
        //     }
        }
    }
}