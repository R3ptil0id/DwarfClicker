using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Ioc;

namespace Utils
{
    public abstract class CustomAfterCompile
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            var scriptableObject = Utility.GetAllInstances<StoreCustomAttributes>().FirstOrDefault();

            if (scriptableObject == null)
            {
                const string assetFolderPath = "Assets/ScriptableObjects/" + nameof(StoreCustomAttributes) + ".asset"; 
                scriptableObject = ScriptableObject.CreateInstance<StoreCustomAttributes>();
                AssetDatabase.CreateAsset(scriptableObject, assetFolderPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            scriptableObject.Types.Clear();
            scriptableObject.NeedInitializeTypes.Clear();

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsDefined(typeof(RegistrateInIocAttribute))).ToList();

            foreach (var type in types)
            {
                scriptableObject.Types.Add(type);

                foreach (var attribute in type.GetCustomAttributes())
                {
                    if (((RegistrateInIocAttribute)attribute).NeedInitialize)
                    {
                        scriptableObject.NeedInitializeTypes.Add(type);
                    }
                }
            }
            EditorUtility.SetDirty(scriptableObject);
            
            var rootObjects = new List<GameObject>();
            var scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            var storedMonoBehaviours = new List<MonoBehaviour>();
            var storedInitializableMonoBehaviours = new List<MonoBehaviour>();
            MonoBehaviourIocInstaller monoBehaviourIocInstaller = null;

            foreach (var rootObject in rootObjects)
            {
                var monobehaviours = rootObject.GetComponentsInParent<MonoBehaviour>();

                foreach (var monobehaviour in monobehaviours)
                {
                    monoBehaviourIocInstaller = monoBehaviourIocInstaller != null
                        ? monoBehaviourIocInstaller
                        : rootObject.GetComponentsInParent<MonoBehaviourIocInstaller>().FirstOrDefault();

                    var type = monobehaviour.GetType();
                    var attribute = type
                        .GetCustomAttributes<RegistrateMonoBehaviourInIocAttribute>().FirstOrDefault();

                    if (attribute == null)
                    {
                        continue;
                    }

                    storedMonoBehaviours.Add(monobehaviour);
                    if (attribute.NeedInitialize)
                    {
                        storedInitializableMonoBehaviours.Add(monobehaviour);
                    }
                }
            }

            if (monoBehaviourIocInstaller == null)
            {
                return;
            }

            monoBehaviourIocInstaller.GameObjects.Clear();
            foreach (var monoBehaviour in storedMonoBehaviours)
            {
                monoBehaviourIocInstaller.GameObjects.Add(monoBehaviour);
            }
            
            monoBehaviourIocInstaller.Initializables.Clear();
            foreach (var monoBehaviour in storedInitializableMonoBehaviours.Where(m => m is IInitializable)) {
                monoBehaviourIocInstaller.Initializables.Add(monoBehaviour);
            }
        }
    }
}