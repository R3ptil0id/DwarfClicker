using Utils.Ioc;

namespace Controllers
{
    public abstract class BaseController 
    {
        public BaseController()
        {
            this.Inject();
        }
    }
}