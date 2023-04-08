using System;
using System.Collections.Generic;
using Data.PerksData;
using Enums;

namespace Models
{
    public class PerksModel
    {
        private readonly Dictionary<PerkType, PerkData> _perks = new();
        private readonly Dictionary<PerkType, LoadedPerkData> _loadedPerks = new();
        
        private readonly Dictionary<PriceCount, int> _priceCounts = new();
        
        public event Action UpdatedNotify;
        
        public List<PerkType> BuyablePerks { get; } = new();
        public List<PerkType> ActivePerks { get; } = new ();
        
        public PerksModel(LoadedPerkData[] loadedPerks)
        {
            foreach (var loadedPerkData in loadedPerks)
            {
                _loadedPerks.Add(loadedPerkData.PerkType, loadedPerkData);
                
                var perk = new PerkData(loadedPerkData);
                _perks.Add(loadedPerkData.PerkType, perk);
                
                if(!_priceCounts.ContainsKey(loadedPerkData.PriceCount) && loadedPerkData.PriceCount != PriceCount.Undefined)
                    _priceCounts.Add(loadedPerkData.PriceCount, 1);
                
                if (loadedPerkData.ActiveOnStart)
                    ActivePerks.Add(loadedPerkData.PerkType);
                else
                    BuyablePerks.Add(loadedPerkData.PerkType);
            }
        }

        public void UpdatePerkPriceCount(PerkType type, int count)
        {
            var priceCount = _loadedPerks[type].PriceCount;
            _priceCounts[priceCount] = count;
        }

        public PerkData GetPerkData(PerkType type)
        {
            return _perks.TryGetValue(type, out var data) ? data : null;
        }
        
        public LoadedPerkData GetLoadedPerkData(PerkType type)
        {
            return _loadedPerks.TryGetValue(type, out var data) ? data : null;
        }

        public void BuyPerk(PerkType perkType)
        {            
            var canBuy = _perks.TryGetValue(perkType, out var data);
            canBuy &= _loadedPerks.TryGetValue(perkType, out var loadedData);
            canBuy &= BuyablePerks.Contains(perkType);  
            
            if (!canBuy)
                return;
   
            var newValue = data.Value + loadedData.Value;
            data.Value = Math.Clamp(newValue, newValue, loadedData.MaxValue);

            if (data.Value >= loadedData.MaxValue)
            {
                ActivePerks.Add(perkType);
                BuyablePerks.Remove(perkType);
            }
            else if (!ActivePerks.Contains(perkType))
            {
                ActivePerks.Add(perkType);
            }
            
            if(!StaticPrice(perkType))
                SetPerkNewPrice(data, loadedData);

            UpdatedNotify?.Invoke();
        }
        
        private void SetPerkNewPrice(PerkData data, LoadedPerkData loadedPerkData)
        {
            var currentPriceCount = 1;
            if (_priceCounts.TryGetValue(loadedPerkData.PriceCount, out var priceCount))
            {
                currentPriceCount = priceCount;
            }
            
            data.Price = loadedPerkData.BasePrice * data.Value * loadedPerkData.Modifier *  currentPriceCount;
        }

        private static bool StaticPrice(PerkType perkType)
        {
            switch (perkType)
            {
                case PerkType.Undefined:
                case PerkType.Currency1Open:
                case PerkType.Currency2Open:
                case PerkType.Currency3Open:
                case PerkType.Currency4Open:
                    return true;
            }

            return false;
        }
    }
}