using System;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc]
    public class InputControl : MonoBehaviour, Iinitializable
    {
        [SerializeField] private float _zoomFactor;
        [SerializeField] private float _zoomLerpSpeed;
        
        private Camera _camera;
        private float _targetZoom;
        public Action NotifyClick;
        
        public void Click()
        {
            NotifyClick?.Invoke();
        }

        public void Initialize()
        {
            _camera = IoC.Resolve<CameraControl>().Camera;
        }
    }
}