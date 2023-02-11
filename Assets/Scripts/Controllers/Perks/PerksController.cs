using System;
using System.Collections.Generic;
using Utils.Ioc;

namespace Controllers.Perks
{
    [RegistrateInIoc(needInitialize: true)]
    public class PerksController : BaseController, IInitializable
    { 
        private readonly Dictionary<Type, object> _perks = new();

        public void Initialize()
        {
            AddPerk<CurrencyBarPerks>();
            AddPerk<BotsPerks>();
            AddPerk<WorkersPerks>();
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