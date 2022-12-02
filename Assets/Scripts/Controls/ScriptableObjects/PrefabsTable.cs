using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PrefabsTable : ScriptableObject
    {
        [Header("Building")] 
        public GameObject MainBlock;
        public GameObject TreasuryBlock;
        
        // [Header("Workers")] 
        [Header("CommonElements")]
        public GameObject Сurrency_1;
        public GameObject Сurrency_2;
        public GameObject Сurrency_3;
        public GameObject Сurrency_4;
    }
}