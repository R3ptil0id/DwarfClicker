using Constants;
using Data.PerksData;
using Utils.Ioc;
using Utils.JsonUtils;

namespace Data
{
    [RegistrateInIoc()]
    public class LoadedData
    {
        public LoadedPerkData[] PerkDataCollection { get; private set; }

        public LoadedData()
        {
            PerkDataCollection = LoadedPerkParser.Parse(PathConstants.PerksConstants);    
        }
    }
}