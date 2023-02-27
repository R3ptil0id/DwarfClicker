using System;
using UnityEngine;
using Utils.Ioc;

namespace Controls.InputsControls
{
    [RegistrateMonoBehaviourInIoc]
    public class InputUiControl : MonoBehaviour
    {
        public Action NotifyClickPerkPanel;

        public void ClickPerkPanel()
        {
            NotifyClickPerkPanel?.Invoke();
        }
    }
}