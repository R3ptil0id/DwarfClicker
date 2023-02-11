using Utils.Ioc;

namespace Controllers
{
    public abstract class BaseController
    {
        protected BaseController()
        {
            this.Inject();
        }
    }
}