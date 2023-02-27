using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [CreateAssetMenu][RegistrateScriptableObjectInIoc]
    public class UiPrefabs : ScriptableObject
    {
        public GameObject BuyPerk;
        public GameObject ActivePerk;
    }
}