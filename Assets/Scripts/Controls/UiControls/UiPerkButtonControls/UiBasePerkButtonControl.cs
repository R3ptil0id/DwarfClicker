using System;
using TMPro;
using UnityEngine;

namespace Controls.UiControls.UiPerkButtonControls
{
    public abstract class UiBasePerkButtonControl : MonoBehaviour, IDisposable
    {
        public TMP_Text PerkTypeText;
        public TMP_Text PerkLevelText;
        public TMP_Text DescriptionText;
        
        public virtual void Dispose()
        {
        }
    }
}