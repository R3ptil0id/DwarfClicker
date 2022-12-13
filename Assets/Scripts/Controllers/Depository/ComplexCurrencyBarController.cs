using System;
using System.Collections.Generic;
using Constants;
using Controls.GameElements.CurrencyBar;
using Enums;
using UnityEngine;

namespace Controllers.Depository
{
    public class ComplexCurrencyBarController : CurrencyBarController
    {
        private List<Action> _listeners;
        private readonly ComplexCurrencyBarControl _complexControl;
        
        public int CurrentBarLvl { get; private set; }
        public Vector3 Position => _control.transform.localPosition;

        public ComplexCurrencyBarController(CurrencyBarControl control) : base(control)
        {
            _listeners = new List<Action>();
            _complexControl = (ComplexCurrencyBarControl)_control;
            _complexControl.NotifyAnimationComplete += OnAnimationComplete;
        }

        public void AddListener(Action listener)
        {
            _listeners.Add(listener);
        }

        public void AddLevel(int lvl)
        {
            CurrentBarLvl += lvl;
            
            if(CurrentBarLvl == DataConstants.CurrencyValues[CurrencyLevel.Units_5])
            {
                CurrencyLevel = CurrencyLevel.Units_5;
                _complexControl.AddLevel(CurrentBarLvl, CurrencyLevel);
            }
            else
            {
                _complexControl.AddLevel(CurrentBarLvl);    
            }
        }    
        
        private void OnAnimationComplete()
        {
            foreach (var listener in _listeners)
            {
                listener?.Invoke();
            }

            _complexControl.NotifyAnimationComplete -= OnAnimationComplete;
            _listeners = null;
        }
    }
}