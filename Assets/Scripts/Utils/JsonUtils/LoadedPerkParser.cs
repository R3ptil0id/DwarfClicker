using System;
using Data.PerksData;
using Enums;
using SimpleJSON;
using UnityEngine;

namespace Utils.JsonUtils
{
    public abstract class LoadedPerkParser
    {
        public static LoadedPerkData[] Parse(string path)
        {
            var targetFile = Resources.Load<TextAsset>(path);
            var forecastNode = JSONNode.Parse(targetFile.text)!;
            var array = forecastNode["DataArray"].AsArray;
            var loadedData = new LoadedPerkData[array.Count];
            
            for (var i = 0; i < array.Count; i++)
            {
                loadedData[i] = new LoadedPerkData();

                var node = array[i];

                loadedData[i].PerkType = Enum.TryParse(node["PerkType"], out PerkType perkType)
                    ? perkType
                    : PerkType.Undefined;
                
                loadedData[i].DependencyPerkType = Enum.TryParse(node["DependencyPerkType"], out PerkType dependencyPerkType)
                    ? dependencyPerkType
                    : PerkType.Undefined;

                loadedData[i].CurrencyType = Enum.TryParse(node["CurrencyType"], out CurrencyType currencyType)
                    ? currencyType
                    : CurrencyType.Undefined;
                
                loadedData[i].PriceCount = Enum.TryParse(node["PriceCount"], out PriceCount priceCount)
                    ? priceCount
                    : PriceCount.Undefined;

                loadedData[i].BasePrice = node["BasePrice"].AsInt;
                loadedData[i].DependencyPerkLevel = node["DependencyPerkLevel"].AsInt;
                loadedData[i].Modifier = node["Modifier"].AsFloat;
                loadedData[i].Value = node["Value"].AsInt;
                loadedData[i].MaxValue = node["MaxValue"].AsInt;
            }

            return loadedData;
        }
    }
}