using System;
using UnityEngine;
using Utils.Ioc;

namespace Controls.InputsControls
{
    [RegistrateMonoBehaviourInIoc]
    public class BotsInputControl : MonoBehaviour
    {    
        public Action NotifyClickAddBot;
        
        public void ClickAddBot()
        {
            NotifyClickAddBot?.Invoke();
        }
    }
}