using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class Utility
    {
        public static T[] GetAllInstances<T>(string folder = "") where T : Object
        {
            string [] guids;
            if (folder != string.Empty)
            {
                guids = AssetDatabase.FindAssets("t:" +
                                                 typeof(T)
                                                     .Name,
                    new[] { folder }); //FindAssets uses tags check documentation for more info
            }
            else
            {
                guids = AssetDatabase.FindAssets("t:" +
                                                 typeof(T)
                                                     .Name); //FindAssets uses tags check documentation for more info
            }

            var instances = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                instances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return instances;
        }
    }
}