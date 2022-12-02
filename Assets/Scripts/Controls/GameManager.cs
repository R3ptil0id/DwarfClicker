using Controllers.GameController;
using UnityEngine;

namespace Controls
{
    public class GameManager : MonoBehaviour
    {
        private GameController _gameController;
        private Installer _installer;
        
        private void Start()
        {
           _gameController = new GameController(_installer);
        }

        private void FixedUpdate()
        {
            _gameController.Update();
        }
    }
}