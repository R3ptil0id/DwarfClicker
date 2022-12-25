using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [CreateAssetMenu][RegistrateScriptableObjectInIoc]
    public class CurrenciesElementsPrefabs : ScriptableObject
    {
        [Header("Currencies_0_Elements")]
        [Space(3)]
        public GameObject Bar0Lvl1;
        public GameObject Bar0Lvl2;
        public GameObject Bar0Lvl3;
        public GameObject Bar0Lvl4;
        
        [Header("Currencies_1_Elements")]
        [Space(3)]
        public GameObject Bar1Lvl1;
        public GameObject Bar1Lvl2;
        public GameObject Bar1Lvl3;
        
        private void Initialize()
        {
            IoC.Register(this);
        }
    }
}