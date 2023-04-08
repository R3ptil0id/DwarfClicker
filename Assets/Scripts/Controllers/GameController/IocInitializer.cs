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
        private List<ILateInitializable> _lateInitializables = new ();

        public IocInitializer(MonoBehaviourIocInstaller iocInstaller)
        {
            IoC.CreateIoC();
            IoC.Register(iocInstaller);
            
            RegistrateScriptableObjects();
            RegistrateMonobehavioursInIoC();
            RegistrateCustomsInIoC();
            
            InitializeMembersInIoc();
        }

        private static void RegistrateScriptableObjects()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsDefined(typeof(RegistrateScriptableObjectInIocAttribute))).ToList();
            var scriptableObjects = Utility.GetAllInstances<ScriptableObject>(CommonConstants.ScriptableObjectFolderPath);

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
            var storeCustomAttributes = IoC.Resolve<StoreCustomAttributes>();
            
            var types = storeCustomAttributes.Types;
            var needInitializeTypes = storeCustomAttributes.NeedInitializeTypes;
            var needLateInitializeTypes = storeCustomAttributes.NeedLateInitializeTypes;
            
            var instances = new List<object>();
                
            foreach (var type in types)
            {
                var instance = IoC.Register(type);
                instances.Add(instance);
               
                if (instance is IInitializable initialize && needInitializeTypes.Contains(type))
                {
                    _initializables.Add(initialize);
                }
                
                if (instance is ILateInitializable lateInitialize && needLateInitializeTypes.Contains(type))
                {
                    _lateInitializables.Add(lateInitialize);
                }
            }
            
            foreach (var instance in instances)
            {
                instance.Inject();
            }
        }

        private void InitializeMembersInIoc()
        {
            foreach (var initializable in _monoBehaviourIocInstaller.Initializables.Where(i => i is IInitializable))
            {
                ((IInitializable)initializable).Initialize();
            }

            foreach (var initializable in _initializables)
            {
                initializable.Initialize();
            }

            foreach (var lateInitializable in _lateInitializables)
            {
                lateInitializable.LateInitialize();
            }
        }
    }
}