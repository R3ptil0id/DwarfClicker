
using UnityEngine;

namespace Controls
{
    public class InputControl : MonoBehaviour
    {
        [SerializeField] private float _zoomFactor;
        [SerializeField] private float _zoomLerpSpeed;
        
        private Camera _camera;
        private float _targetZoom;

        private void Awake()
        {
            _camera = CameraControl.Camera;
        }

    }
}