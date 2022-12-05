using UnityEngine;

namespace Controls.ScriptableObjects
{
    [CreateAssetMenu]
    public class CommonPrefabs : ScriptableObject
    {
        [Header("Building")] 
        public GameObject MainBlock;
        public GameObject TreasuryBlock;
    }
}