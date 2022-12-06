using System.Collections.Generic;
using System.Linq;
using Controls;
using Controls.GameElements.CurrencyBar;
using Controls.ScriptableObjects;
using Enums;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class CurrencyObjectsPool
    {
        private readonly CurrenciesElementsPrefabs _currenciesPrefabs;
        private readonly Transform _parentObject;

        private readonly Dictionary<CurrencyType, Dictionary<CurrencyLevel, List<CurrencyBarControl>>> _dictionary;
        public CurrencyObjectsPool(Installer installer, int startCount = 20)
        {
            _parentObject = installer.PoolObject;
            _currenciesPrefabs = installer.PrefabsTable.CurrenciesElementsPrefabs;
            _dictionary = new Dictionary<CurrencyType, Dictionary<CurrencyLevel, List<CurrencyBarControl>>>();
            CreateDictionary();
        }

        public CurrencyBarControl GetCurrencyObject(CurrencyType type, CurrencyLevel level)
        {
            foreach (var control in _dictionary[type][level].Where(control => !control.IsBusy))
            {
                control.Busy();
                return control;
            }

            return null;
        }

        public void Release(CurrencyBarControl control)
        {
            control.Release();
        }

        public void Initialize()
        {
            for (var i = 0; i < 20; i++)
            {
                var type = _currenciesPrefabs.GetType();
                var fields = type.GetFields();

                foreach (var field in fields)
                {
                    var gameObject = (GameObject)field.GetValue(_currenciesPrefabs);
                    var component = gameObject.GetComponent<CurrencyBarControl>();
                    component.Initialzie(_parentObject.position);
                    _dictionary[component.CurrencyType][component.CurrencyLevel].Add(component);
                    Object.Instantiate(gameObject, _parentObject);
                }
            }
        }

        private void CreateDictionary()
        {   
            foreach (var currencyType in EnumExtension.GetAllItems<CurrencyType>())
            {   
                var dict = new Dictionary<CurrencyLevel, List<CurrencyBarControl>>();
                foreach (var currencyLevel in EnumExtension.GetAllItems<CurrencyLevel>())
                {
                    var list = new List<CurrencyBarControl>();
                    dict.Add(currencyLevel, list);
                }   
                
                _dictionary.Add(currencyType, dict);
            }
        }
    }
}