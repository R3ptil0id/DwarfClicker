using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc]
    public class ObjectsInstaller : MonoBehaviour
    {
        [Header("SceneObjects")]
        public Transform PoolObject;
        public Transform Currencies;
    }
}