using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [CreateAssetMenu][RegistrateScriptableObjectInIoc]
    public class CurrenciesElementsPrefabs : ScriptableObject
    {
        [Header("Currencies_Elements")]
        [Space(3)]
        public GameObject Bar0;
        public GameObject Bar1;
        public GameObject Bar2;
        
        [HideInInspector]
        public int Count;
    }
}