using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CurrenciesElementsPrefabs))]
    public class CurrenciesElementsPrefabsEditor : UnityEditor.Editor
    {
        private string _assetFolderPath = "Assets/Prefabs/Currencies";
        private int _breakRecursion = 200;        
        
        public override void OnInspectorGUI()
        {
            var currenciesPrefabs = (CurrenciesElementsPrefabs)target;
            
            GUILayout.Label("Path Prefabs Folder");
            _assetFolderPath = EditorGUILayout.TextField(_assetFolderPath);
            
            GUILayout.Space(10);
            DrawDefaultInspector();
            GUILayout.Space(10);
            if(GUILayout.Button("Fill"))
            {
                var list = new List<GameObject>();
                FillList(_assetFolderPath, 0, ref list);
                FillFields(list, currenciesPrefabs.GetType());
            }
        }

        private void FillList(string path, int currentPass, ref List<GameObject> list)
        {
            if (currentPass > _breakRecursion)
            {
                return;
            }

            var names = Directory.GetFiles(path).Where(f => !f.Contains("meta"));

            list.AddRange(names.Select(AssetDatabase.LoadAssetAtPath<GameObject>));

            var subFolders = AssetDatabase.GetSubFolders(path);
            foreach (var subFolder in subFolders)
            {
                FillList(subFolder, currentPass, ref list);
            }
        }

        private void FillFields(IList<GameObject> list, Type currenciesElementsPrefabs)
        {
            var fields = currenciesElementsPrefabs.GetFields();
         
            foreach (var fieldInfo in fields)
            {
                var index = -1;
                for (var i = 0; i < list.Count; i++)
                {
                    if (fieldInfo.Name != list[i].name) continue;
                    fieldInfo.SetValue(target,list[i]);
                    index = i;
                }

                if (index >= 0)
                {
                    list.RemoveAt(index);
                }
            }
        }
    }
}