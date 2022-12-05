using UnityEngine;

namespace Controls.GameElements
{
    public class ShaftControl : MonoBehaviour
    {
        public void AddLevel()
        {
            transform.localScale += Vector3.up * 0.056f;
        }
    }
}