using Controls;
using Controls.GameElements.Workers;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controllers.Workers
{
    public class WorkerController : BaseController
    {
        [Inject] private ObjectsInstaller _objectsInstaller;

        protected readonly WorkerControl _control;

        public WorkerType WorkerType { get; protected set; }

        public WorkerController(WorkerControl control, Vector3 position)
        {
            _control = control;
            _control.transform.SetParent(_objectsInstaller.MinerShaftStartPoint);
            _control.Initialize();
            _control.SetToPosition(position);
            
            WorkerType = control.WorkerType;
        }
    }
}