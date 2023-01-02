using Controls.GameElements;
using Controls.UiControls;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc]
    public class Installer : MonoBehaviour
    {
        [Header("SceneObjects")]
        public Transform PoolObject;
        public Transform Currencies;
        
        [Header("ScriptableObjects")]
        public StoreCustomAttributes storeCustomAttributes;
        public CurrenciesElementsPrefabs CurrenciesElementsPrefabs;
        
        [Header("Controls")]
        public CameraControl CameraControl;
        public CurrenciesUiControl CurrenciesUiControl;
        public InputControl InputControl;
        public UiControl UiControl;
        public ShaftControl ShaftControl;
        public DepositoryControl DepositoryControl;
    }
}