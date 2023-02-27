using System.Collections.Generic;
using Constants;
using Controllers.Perks;
using Controls;
using Controls.InputsControls;
using Enums;
using UnityEngine;
using Utils.EnumExtensions;
using Utils.Ioc;

namespace Controllers.Workers
{
    public class WorkersController : BaseController
    {
        [Inject] private readonly ObjectsInstaller _objectsInstaller;
        [Inject] private readonly PerksController _perksController;
        [Inject] private readonly WorkersInputsControl _inputControl;
        
        private readonly WorkersPoolController _workersPoolController;
        private readonly Dictionary<WorkerType, List<WorkerController>> _workers = new();
        private Vector3 _position; 
        
        public WorkersController()
        {
            foreach (var currencyType in EnumExtension.GetAllItems<WorkerType>())
            {
                _workers.Add(currencyType, new List<WorkerController>());
            }

            _position = _objectsInstaller.MinerShaftStartPoint.position;
            
            _workersPoolController = new WorkersPoolController();
            _inputControl.NotifyClickAddMiner += ClickAddMinerHandler;
        }

        private void ClickAddMinerHandler(WorkerType workerType)
        {
            if (_workers.TryGetValue(workerType, out var workControllers) &&
                workControllers.Count >= _perksController.GetPerk<WorkersPerks>().MinersMaxCount)
            {
                return;
            }

            var workerControl = _workersPoolController.GetWorkerObject(WorkerType.Miner);
            var workController = new WorkerController(workerControl, _position);
            _workers[workerType].Add(workController);
            _position += Vector3.left * WorkersDataPerkConstants.XworkerOffset;
        }
    }
}