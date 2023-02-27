using UnityEngine;
using Utils.Ioc;

namespace Controls.UiControls
{
    [RegistrateMonoBehaviourInIoc]
    public class UiPerksControl : MonoBehaviour
    {
        public RectTransform PerksPanel;
        
        [Space(6)]
        public RectTransform BuyingPerksContent;
        public RectTransform ActivePerksContent;
    }
}