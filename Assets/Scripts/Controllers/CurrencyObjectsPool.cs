using Controls;
using Controls.ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class CurrencyObjectsPool
    {
        private readonly PrefabsTable _prefabsTable;
        private readonly Transform _parentObject;

        public CurrencyObjectsPool(Installer installer, PrefabsTable prefabsTable)
        {
            _parentObject = installer.PoolObject;
            _prefabsTable = prefabsTable;
        }

        public void Initialize(int startCount = 20)
        {
            for (var i = 0; i < 20; i++)
            {
                // Object.Instantiate()   
            }
        }
    }
}