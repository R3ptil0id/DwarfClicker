using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controls.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CurrenciesElementsPrefabs))]
    public class CurrenciesElementsPrefabsEditor : UnityEditor.Editor
    {
        private string _assetFolderPath = "Assets/Prefabs/Currencies";
        private int _breakRcursion = 200;        
        //Currency_0_Bar_5

        public override void OnInspectorGUI()
        {
            var currenciesPrefabs = (CurrenciesElementsPrefabs)target;
            DrawDefaultInspector();
            GUILayout.Space(10);
            if(GUILayout.Button("Fill"))
            {
                var currentPass = 0;
                var list = new List<GameObject>();     
                var folders = AssetDatabase.GetSubFolders(_assetFolderPath);
                foreach (var folder in folders)
                {
                    Recursive(folder, list, currentPass);
                }

                FillFields(list, currenciesPrefabs.GetType());
            }
        }

        private void Recursive(string folder, List<GameObject> list, int currentPass)
        {
            if (currentPass > _breakRcursion)
            {
                return;
            }

            var names = Directory.GetFiles(folder).Where(f => !f.Contains("meta"));
            
            list.AddRange(names.Select(AssetDatabase.LoadAssetAtPath<GameObject>));

            var folders = AssetDatabase.GetSubFolders(folder);
            foreach (var fld in folders)
            {
                Recursive(fld, list, currentPass);
            }
        }
        
        

        private void FillFields(List<GameObject> list, Type currenciesElementsPrefabs)
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