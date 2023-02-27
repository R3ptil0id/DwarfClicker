using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Controllers.UiControllers;
using Controls.InputsControls;
using Controls.UiControls;
using Enums;
using ScriptableObjects;
using Utils.Ioc;

namespace Controllers.Perks
{
    [RegistrateInIoc(needInitialize: true)]
    public class PerksController : BaseController, IInitializable
    {
        [Inject] private readonly UiPerksControl _perksControl;
        [Inject] private readonly InputUiControl _inputUiControl;
        [Inject] private readonly UiPrefabs _uiPrefabs;
        
        private readonly Dictionary<Type, object> _perks = new();
        
        private Dictionary<PerkType, ConstantPerkData> _notActivePerks = new(); 
        private Dictionary<PerkType, ConstantPerkData> _activePerks = new(); 
        
        private UiPerksController _uiPerksController;

        public void Initialize()
        {
            AddPerk<CurrencyBarPerks>();
            AddPerk<BotsPerks>();
            AddPerk<WorkersPerks>();

            var perksList = _perks.Values.Cast<IPerks>().ToList();

            _uiPerksController = new UiPerksController(perksList);
        }

        public void AddPerk<T>() where T : new()
        {
            _perks.Add(typeof(T), new T());
        }

        public T GetPerk<T>()
        {
            if(_perks.TryGetValue(typeof(T), out var obj))
            {
                return (T)obj;
            }

            return default;
        }
    }
}