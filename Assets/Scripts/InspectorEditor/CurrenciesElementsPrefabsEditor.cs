using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace InspectorEditor
{
    [CustomEditor(typeof(CurrenciesElementsPrefabs))]
    public class CurrenciesElementsPrefabsEditor : Editor
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
                EditorUtility.SetDirty(target);
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
            ((CurrenciesElementsPrefabs)target).Count = 0;
            
            foreach (var fieldInfo in fields)
            {
                for (var i = list.Count-1; i >= 0; i--)
                {
                    if (fieldInfo.Name != list[i].name) continue;
                    fieldInfo.SetValue(target,list[i]);
                    ((CurrenciesElementsPrefabs)target).Count++; 
                    list.RemoveAt(i);
                }
            }
        }
    }
}