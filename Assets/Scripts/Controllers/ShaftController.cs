using Controls.GameElements;

namespace Controllers
{
    public class ShaftController
    {
        private readonly ShaftControl _shaftControl;

        public ShaftController(ShaftControl shaftControl)
        {
            _shaftControl = shaftControl;
        }

        public void AddLevel()
        {
            _shaftControl.AddLevel();
        }
    }
}