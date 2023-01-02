using Controllers.GameController;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    public class GameManager : MonoBehaviour
    {
        private GameController _gameController;
        
        private void Awake()
        {
            IoC.CreateIoC();
            IoC.Register(GetComponent<MonoBehaviourIocInstaller>());

            _gameController = new GameController();
            _gameController.PreInitialize();
        }
        
        private void Start()
        {
            _gameController.Initialize();
        }
    }
}