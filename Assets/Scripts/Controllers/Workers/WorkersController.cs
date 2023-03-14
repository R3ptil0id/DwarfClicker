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
        [Inject] private ObjectsInstaller _objectsInstaller;
        [Inject] private PerksController _perksController;
        [Inject] private WorkersInputsControl _inputControl;
        
        private readonly Dictionary<WorkerType, List<WorkerController>> _workers = new();
        private readonly WorkersPoolController _workersPoolController;
        
        private Vector3 _position; 
         
        public WorkersController()
        {
            foreach (var currencyType in EnumExtension.GetAllItems<WorkerType>())
            {
                _workers.Add(currencyType, new List<WorkerController>());
            }

            _position = _objectsInstaller.MinerShaftStartPoint.position;
            
            _workersPerks = _perksController.GetPerksData<WorkersPerks>();
            _workersPoolController = new WorkersPoolController();
            
            _inputControl.NotifyClickAddMiner += ClickAddMinerHandler;
        }

        private void ClickAddMinerHandler(WorkerType workerType)
        {
            
            if (_workers.TryGetValue(workerType, out var workControllers) &&
                workControllers.Count >= _workersPerks.GetConstantValue(PerkType.StartMinersLvl1Count).Value)
            {
                return;
            }

            var workerControl = _workersPoolController.GetWorkerObject(WorkerType.Miner);
            var workController = new WorkerController(workerControl, _position);
            
            _workers[workerType].Add(workController);
            _position += Vector3.left * CommonConstants.XworkerOffset;
        }
    }
}