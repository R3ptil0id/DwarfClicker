using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Constants;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Utils.Ioc;

namespace Controllers.GameController
{
    public class IocInitializer
    {
        private MonoBehaviourIocInstaller _monoBehaviourIocInstaller;
        private List<IInitializable> _initializables = new ();

        public IocInitializer(MonoBehaviourIocInstaller iocInstaller)
        {
            IoC.CreateIoC();
            IoC.Register(iocInstaller);
            
            RegistrateScriptableObjects();
            RegistrateMonobehavioursInIoC();
            RegistrateCustomsInIoC();
            
            foreach (var initializable in _monoBehaviourIocInstaller.Initializables.Where(i => i is IInitializable))
            {
                ((IInitializable)initializable).Initialize();
            }
            
            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }
        }

        private static void RegistrateScriptableObjects()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsDefined(typeof(RegistrateScriptableObjectInIocAttribute))).ToList();
            var scriptableObjects = Utility.GetAllInstances<ScriptableObject>(DataConstants.ScriptableObjectFolderPath);

            foreach (var scriptableObject in scriptableObjects)
            {
                foreach (var t in types.Where(t => t == scriptableObject.GetType()))
                {
                    IoC.Register(scriptableObject);
                }
            }
        }
        
        private void RegistrateMonobehavioursInIoC()
        {
            _monoBehaviourIocInstaller = IoC.Resolve<MonoBehaviourIocInstaller>();
            var monoBehaviours = _monoBehaviourIocInstaller.GameObjects;
            foreach (var monoBehaviour in monoBehaviours)
            {
                IoC.Register(monoBehaviour);
            }
        }
        
        private void RegistrateCustomsInIoC()
        {
            var types = IoC.Resolve<StoreCustomAttributes>().Types;
            var needInitializeTypes = IoC.Resolve<StoreCustomAttributes>().NeedInitializeTypes;

            foreach (var type in types)
            {
                var instance = IoC.Register(type);
               
                if (instance is not IInitializable initialize || !needInitializeTypes.Contains(type))
                {
                    continue;
                }
               
                _initializables.Add(initialize);
            }
        }
    }
}