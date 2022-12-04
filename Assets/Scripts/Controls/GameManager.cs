using Controllers.GameController;
using UnityEngine;

namespace Controls
{
    public class GameManager : MonoBehaviour
    {
        private GameController _gameController;
        private Installer _installer;

        private void Awake()
        {
            _installer = GetComponent<Installer>();
        }

        private void Start()
        {
           _gameController = new GameController(_installer);
        }
    }
}