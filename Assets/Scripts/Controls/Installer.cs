using Controls.UiControls;
using MonoBehaviours.GameElements;
using ScriptableObjects;
using UnityEngine;

namespace Controls
{
    public class Installer : MonoBehaviour
    {
        [Header("Commons")]
        public PrefabsTable PrefabsTable;
        
        [Header("Controls")]
        public CameraControl CameraControl;
        public CurrenciesControl CurrenciesControl;
        public InputControl InputControl;
        public UiControl UiControl;
        public ShaftControl ShaftControl;
        public DepositoryControl DepositoryControl;
    }
}