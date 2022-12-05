using Controls;
using UnityEngine;

namespace Controllers
{
    public class EconomicController
    {
        private readonly Vector3 _startPos;
        
        public EconomicController(Installer installer)
        {
            _startPos = installer.DepositoryStartTransform.position;
        }
    }
}