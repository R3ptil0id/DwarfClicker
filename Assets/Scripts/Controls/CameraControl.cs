using UnityEngine;

namespace Controls
{
    public class CameraControl : MonoBehaviour
    {
        public static Camera Camera { get; private set; }

        private void Awake() => Camera = GetComponent<Camera>();
    }
}