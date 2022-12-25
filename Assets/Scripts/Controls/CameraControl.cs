using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    [RegistrateMonoBehaviourInIoc]
    public class CameraControl : MonoBehaviour
    {
        public Camera Camera { get; private set; }

        private void Awake() => Camera = GetComponent<Camera>();
    }
}