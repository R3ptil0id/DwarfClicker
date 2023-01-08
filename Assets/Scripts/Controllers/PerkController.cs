using System.Collections.Generic;
using Constants;
using Enums;
using Utils.Ioc;

namespace Controllers
{
    [RegistrateInIoc(needInitialize: true)]
    public class PerkController : BaseController, IInitializable
    {
        private readonly List<Perks> _activePerks;
        
        private readonly Dictionary<CurrencyType, int> _maxCurrencyBars = new();
        public List<Perks> ActiveActivePerks => _activePerks;

        public Dictionary<CurrencyType, int> MaxCurrencyBars => _maxCurrencyBars;

        public PerkController()
        {
            _activePerks = new List<Perks> { Perks.AutoConvert };
        }

        public void Initialize()
        {
            _maxCurrencyBars.Add(CurrencyType.Currency_0, DataConstants.MaxCurrency0BarOnStart);
            _maxCurrencyBars.Add(CurrencyType.Currency_1, DataConstants.MaxCurrency1BarOnStart);
            _maxCurrencyBars.Add(CurrencyType.Currency_2, DataConstants.MaxCurrency2BarOnStart);
        }
    }
}