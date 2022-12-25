using ScriptableObjects;
using Services.GameLoop;
using Utils.Ioc;

namespace Controllers.GameController
{
    public class GameController : IUpdateListener
    {
        public void Update(float deltaTime)
        {
            
        }

        public void Initialize()
        {
            var types = IoC.Resolve<StoreCustomAttributesTypes>().Types;

            foreach (var type in types)
            {
                IoC.Register(type);
            }
            
            IoC.Resolve<GameLoopService>().Register(this);
        }
    }
}