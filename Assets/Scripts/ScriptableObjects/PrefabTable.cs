using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [CreateAssetMenu][RegistrateScriptableObjectInIoc]
    public class PrefabTable : ScriptableObject
    {
        public GameObject Bot;
    }
}