using Constants;
using Data.PerksData;
using Utils.Ioc;
using Utils.JsonUtils;

namespace Data
{
    [RegistrateInIoc(true)]
    public class LoadedData : IInitializable
    {
        public PerkData[] PerkDataCollection { get; private set; }

        public void Initialize()
        {
            PerkDataCollection = JsonHelper.GetData<PerkData>(PathConstants.PerksConstants);
        }
    }
}