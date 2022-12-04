using Controls.GameElements;

namespace Controllers
{
    public class DepositoryController
    {
        private readonly DepositoryControl _depositoryController;

        public DepositoryController(DepositoryControl depositoryController)
        {
            _depositoryController = depositoryController;
        }
    }
}