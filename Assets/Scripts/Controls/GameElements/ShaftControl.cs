using UnityEngine;

namespace MonoBehaviours.GameElements
{
    public class ShaftControl : MonoBehaviour
    {
        public void AddLevel()
        {
            transform.localScale += Vector3.up * 1.25f;
        }
    }
}