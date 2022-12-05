using System.Collections.Generic;
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
        public CurrencyObjectsPool(Installer installer, PrefabsTable prefabsTable, int startCount = 20)
        {
            _parentObject = installer.PoolObject;
            _currenciesPrefabs = prefabsTable.CurrenciesElementsPrefabs;
            _dictionary = new Dictionary<CurrencyType, Dictionary<CurrencyLevel, List<CurrencyBarControl>>>();
            CreateDictionary();
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
                    _dictionary[component.CurrencyType][component.CurrencyLevel].Add(component);
                    Object.Instantiate(gameObject, _parentObject);
                }
            }
        }
    }
}