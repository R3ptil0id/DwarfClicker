using System;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc(needInitialize: true)]
    public class InputControl : MonoBehaviour, IInitializable
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
            var cameraControl = IoC.Resolve<CameraControl>();
            _camera = cameraControl.Camera;
        }
    }
}