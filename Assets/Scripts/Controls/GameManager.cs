using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Controllers.GameController;
using UnityEngine;
using Utils;
using Utils.Ioc;

namespace Controls
{
    public class GameManager : MonoBehaviour
    {
        private GameController _gameController;
        private const string ScriptableObjectFolderPath = "Assets/ScriptableObjects";
        
        private void Awake()
        {
            IoC.Initialize();
            RegistrateInIoC();

            _gameController = new GameController();
        }

        private void RegistrateInIoC()
        {
            var monoBehTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsDefined(typeof(RegistrateMonoBehaviourInIocAttribute))).ToList();
            var monoBehaviours = GetComponents<MonoBehaviour>();
            var initializables = new List<Iinitializable>();
            
            foreach (var monoBehaviour in monoBehaviours)
            {
                foreach (var t in monoBehTypes.Where(t => t == monoBehaviour.GetType()))
                {
                    if (monoBehaviour is Iinitializable behaviour)
                    {
                        initializables.Add(behaviour);
                    }

                    IoC.Register(monoBehaviour);
                }
            }

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsDefined(typeof(RegistrateScriptableObjectInIocAttribute))).ToList();
            var scriptableObjects = Utility.GetAllInstances<ScriptableObject>(ScriptableObjectFolderPath);

            foreach (var scriptableObject in scriptableObjects)
            {
                foreach (var t in types.Where(t => t == scriptableObject.GetType()))
                {
                    IoC.Register(scriptableObject);
                }
            }
            
            foreach (var initializable in initializables)        
            {
                initializable.Initialize();
            }
        }

        private void Start()
        {
            _gameController.Initialize();
        }
    }
}