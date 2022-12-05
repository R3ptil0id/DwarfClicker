using Controls.GameElements;
using Controls.ScriptableObjects;
using Controls.UiControls;
using MonoBehaviours.GameElements;
using UnityEngine;

namespace Controls
{
    public class Installer : MonoBehaviour
    {
        [Header("Commons")]
        public PrefabsTable PrefabsTable;
        
        [Header("Controls")]
        public CameraControl CameraControl;
        public CurrenciesUiControl currenciesUiControl;
        public InputControl InputControl;
        public UiControl UiControl;
        public ShaftControl ShaftControl;
        public DepositoryControl DepositoryControl;
        
        [Header("Markers")]
        public Transform DepositoryStartTransform;
        public Transform PoolObject;
    }
}