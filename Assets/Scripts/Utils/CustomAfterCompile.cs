using System.Linq;
using System.Reflection;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using Utils.Ioc;

namespace Utils
{
    public abstract class CustomAfterCompile
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsDefined(typeof(RegistrateInIocAttribute)));
            var scriptableObject = Utility.GetAllInstances<StoreCustomAttributesTypes>().FirstOrDefault();

            if (scriptableObject == null)
            {
                var assetFolderPath = "Assets/ScriptableObjects/" + nameof(StoreCustomAttributesTypes) + ".asset"; 
                scriptableObject = ScriptableObject.CreateInstance<StoreCustomAttributesTypes>();
                AssetDatabase.CreateAsset(scriptableObject, assetFolderPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            
            scriptableObject.Types.Clear();
            foreach (var type in types)
            {
                scriptableObject.Types.Add(type);
            }
        }
    }
}