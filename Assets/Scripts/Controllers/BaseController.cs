using Utils.Ioc;

namespace Controllers
{
    public abstract class BaseController : IInitializable
    {
        public virtual void Initialize()
        {
            this.Inject();
        }
    }
}