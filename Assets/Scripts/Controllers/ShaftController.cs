using Controls;
using Controls.GameElements;

namespace Controllers
{
    public class ShaftController
    {
        private readonly ShaftControl _shaftControl;

        public ShaftController(Installer installer)
        {
            _shaftControl = installer.ShaftControl;
        }

        public void AddLevel()
        {
            _shaftControl.AddLevel();
        }
    }
}