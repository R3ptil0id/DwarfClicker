using Controllers.GameController;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    public class GameStarter : MonoBehaviour
    {
        private GameController _gameController;
        private IocInitializer _iocInitializer;
        
        private void Awake()
        {
            _iocInitializer = new IocInitializer(GetComponent<MonoBehaviourIocInstaller>());
        }
        
        private void Start()
        {
            _gameController = new GameController();
        }
    }
}