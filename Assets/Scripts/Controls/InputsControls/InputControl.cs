using System;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controls.InputsControls
{
    [RegistrateMonoBehaviourInIoc(needInitialize: true)]
    public class InputControl : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _zoomFactor;
        [SerializeField] private float _zoomLerpSpeed;

        private Camera _camera;
        private float _targetZoom;

        
        public void Initialize()
        {
            var cameraControl = IoC.Resolve<CameraControl>();
            _camera = cameraControl.Camera;
        }
    }
}