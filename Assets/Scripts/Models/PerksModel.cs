using System;
using System.Collections.Generic;
using Constants;
using Data.PerksData;
using Enums;

namespace Models
{
    public class PerksModel
    {   
        private readonly Dictionary<PerkType, PerkData> _perks = new();
        private readonly Dictionary<PerkType, LoadedPerkData> _loadedPerks = new();
        private readonly Dictionary<PriceCount, int> _priceCounts = new();

        private const int COne = 1;
        
        public event Action UpdatedNotify;
        
        public List<PerkType> BuyablePerks { get; } = new();
        public List<PerkType> ActivePerks { get; } = new ();
        
        public PerksModel(IEnumerable<LoadedPerkData> loadedPerks)
        {
            foreach (var loadedPerkData in loadedPerks)
            {
                _loadedPerks.Add(loadedPerkData.PerkType, loadedPerkData);
                _perks.Add(loadedPerkData.PerkType, new PerkData(loadedPerkData));
            }

            foreach (var perk in _perks.Values)
            {
                FillCurrentPerk(perk);    
            }
        }

        private void FillCurrentPerk(PerkData perkData)
        {
            if (!_loadedPerks.TryGetValue(perkData.PerkType, out var loadedPerkData))
                return;

            if(!IsDependendentPerkOpened(loadedPerkData))
                return;

            if (!_priceCounts.ContainsKey(loadedPerkData.PriceCount) ||
                loadedPerkData.PriceCount == PriceCount.Undefined)
                _priceCounts.Add(loadedPerkData.PriceCount, COne);

            if (loadedPerkData.Value > CommonConstants.CZero)
                ActivePerks.Add(loadedPerkData.PerkType);

            if (perkData.Value < loadedPerkData.MaxValue)
                BuyablePerks.Add(loadedPerkData.PerkType);
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
            data.Value = Math.Min(newValue, loadedData.MaxValue);
            data.Level++;

            if (data.Value >= loadedData.MaxValue)
            {
                ActivePerks.Add(perkType);
                BuyablePerks.Remove(perkType);
            }
            else if (!ActivePerks.Contains(perkType))
            {
                ActivePerks.Add(perkType);
            }
            
            if(loadedData.PriceCount != PriceCount.Undefined)
                SetPerkNewPrice(data, loadedData);

            foreach (var loadedPerkData in _loadedPerks.Values)      
            {
                if (IsDependendentPerkOpened(loadedPerkData))
                {
                    BuyablePerks.Add(loadedPerkData.PerkType);
                }
            }

            UpdatedNotify?.Invoke();
        }
        
        private void SetPerkNewPrice(PerkData data, LoadedPerkData loadedPerkData)
        {
            var currentPriceCount = _priceCounts.TryGetValue(loadedPerkData.PriceCount, out var priceCount) ? priceCount : COne;
            
            data.Price = loadedPerkData.BasePrice * loadedPerkData.Modifier *  currentPriceCount;
            _priceCounts[loadedPerkData.PriceCount] = ++currentPriceCount;
        }
        
        private bool IsDependendentPerkOpened(LoadedPerkData loadedPerkData)
        {
            if (loadedPerkData.DependencyPerkType == PerkType.Undefined)
                return true;

            return _perks.TryGetValue(loadedPerkData.DependencyPerkType, out var dependencyPerk) &&
                   dependencyPerk.Value >= loadedPerkData.DependencyPerkLevel;
        }

    }
}