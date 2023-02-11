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
        
        [Space(3)][Header("BotObjects")]
        public Transform HomePoint;
        public Transform UnloadPoint;
        public Transform AirPoint;
        public Transform TargetPoint;
        
        [Space(3)][Header("ShaftPositions")]
        public Transform MinerShaftStartPoint;
    }
}