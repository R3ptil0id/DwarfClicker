using System;
using System.Collections.Generic;
using Controls.GameElements;
using Controls.ScriptableObjects;
using Controls.UiControls;
using UnityEngine;
using Object = System.Object;

namespace Controls
{
    public class Installer : MonoBehaviour
    {
        [Header("Commons")]
        public PrefabsTable PrefabsTable;
        public Transform PoolObject;
        
        [Header("Controls")]
        public CameraControl CameraControl;
        public CurrenciesUiControl CurrenciesUiControl;
        public InputControl InputControl;
        public UiControl UiControl;
        public ShaftControl ShaftControl;
        public DepositoryControl DepositoryControl;
        
        [Header("Markers")]
        public Transform DepositoryStartTransform;
        
        private Dictionary<Type, Object> _instances = new();
        
        public void AddInstance(Object obj)
        {
            _instances.Add(obj.GetType(), obj);
        }
        
        public T GetInstance<T>()
        {
            return (T)_instances[typeof(T)];
        }
    }
}