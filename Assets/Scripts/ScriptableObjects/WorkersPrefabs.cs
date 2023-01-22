using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [CreateAssetMenu][RegistrateScriptableObjectInIoc]
    public class WorkersPrefabs : ScriptableObject
    {
        [Header("Workers")]
        [Space(3)]
        public GameObject Miner;
    }
}