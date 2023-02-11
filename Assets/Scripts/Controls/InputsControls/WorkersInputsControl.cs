using System;
using Enums;
using UnityEngine;
using Utils.Ioc;

namespace Controls.InputsControls
{
    [RegistrateMonoBehaviourInIoc]
    public class WorkersInputsControl : MonoBehaviour
    {
        public Action<WorkerType> NotifyClickAddMiner;
        
        public void ClickAddMiner()
        {
            NotifyClickAddMiner?.Invoke(WorkerType.Miner);
        }
    }
}
