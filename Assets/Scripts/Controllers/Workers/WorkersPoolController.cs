using Utils.Ioc;

namespace Controllers.Workers
{
    [RegistrateInIoc(needInitialize: true)]
    public class WorkersPoolController : BaseController, IInitializable
    {
        public void Initialize()
        {
            
        }
    }
}