using Constants;
using Data.PerksData;
using Utils.Ioc;
using Utils.JsonUtils;

namespace Data
{
    [RegistrateInIoc()]
    public class LoadedData
    {
        public PerkData[] PerkDataCollection { get; private set; }

        public LoadedData()
        {
            PerkDataCollection = JsonHelper.GetData<PerkData>(PathConstants.PerksConstants);    
        }
    }
}