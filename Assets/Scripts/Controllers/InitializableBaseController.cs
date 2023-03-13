namespace Controllers
{
    public class InitializableBaseController : BaseController
    {
        public InitializableBaseController()
        {
            Initialize();
        }
    }
}