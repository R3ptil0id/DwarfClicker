using System;
using System.Collections.Generic;
using System.Linq;
using Controls;
using Controls.GameElements.CurrencyBar;
using Controls.GameElements.Workers;
using Enums;
using ScriptableObjects;
using UnityEngine;
using Utils.Ioc;

using Object = UnityEngine.Object;

namespace Controllers.Workers
{
    public class WorkersPoolController : InitializableBaseController
    {
        [Inject] private readonly WorkersPrefabs _workersPrefabs;
        [Inject] private readonly ObjectsInstaller _objectsInstaller;

        private Transform _parentObject;
        private Dictionary<WorkerType, List<WorkerControl>> _dictionary;

        private const int InstancesCount = 20;

        public WorkersPoolController()
        {
            _parentObject = _objectsInstaller.PoolObject;
            _dictionary = new Dictionary<WorkerType, List<WorkerControl>>();

            var type = _workersPrefabs.GetType();
            var fields = type.GetFields();

            foreach (var field in fields)
            {
                var prefab = field.GetValue(_workersPrefabs) as GameObject;
                var workerType = WorkerType.Undefined;
                var list = new List<WorkerControl>();

                if (prefab == null)
                {
                    break;
                }

                for (var j = 0; j < InstancesCount; j++)
                {
                    var instance = Object.Instantiate(prefab, _parentObject);
                    var component = instance.GetComponent<WorkerControl>();
                    component.Initialize();

                    if (component.WorkerType == WorkerType.Undefined)
                    {
                        Debug.LogError($"{prefab} has component.CurrencyType: {component.WorkerType}");
                        continue;
                    }

                    workerType = component.WorkerType;
                    component.Initialize(_parentObject.position);
                    list.Add(component);
                }

                if (workerType == WorkerType.Undefined)
                {
                    continue;
                }

                if (_dictionary.ContainsKey(workerType))
                {
                    throw new ArgumentException($"{workerType} already added to Currencies pool");
                }

                _dictionary.Add(workerType, list);
            }
        }

        public WorkerControl GetWorkerObject(WorkerType type)
        {
            var control = _dictionary[type].FirstOrDefault(c => !c.IsBusy);
            if (control != null)
            {
                control.Busy();
            }

            return control;
        }

        public void Release(CurrencyBarControl control)
        {
            control.Release();
        }
    }
}