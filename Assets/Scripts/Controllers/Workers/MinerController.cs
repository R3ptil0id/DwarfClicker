using Controls.GameElements.Workers;
using UnityEngine;

namespace Controllers.Workers
{
    public class MinerController : WorkerController
    {
        // private MinerControl _minerControl;
        private WorkerControl _minerControl;

        public MinerController(WorkerControl control, Vector3 position) : base(control, position)
        {
            // _minerControl = (MinerControl)_control;
            _minerControl = (WorkerControl)_control;
        }
    }
}