using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InspectorEditor
{
    public class BasePrefabsEditor<T> : Editor where T : UnityEngine.Object
    {
        private int _breakRecursion = 200;

        protected virtual string GetPath()
        {
            return string.Empty;
        }

        public override void OnInspectorGUI()
        {
            var prefabs = (T)target;
            
            GUILayout.Label("Path Prefabs Folder");
            //_assetFolderPath = EditorGUILayout.TextField(GetPaths());
            
            EditorUtility.SetDirty(this);
            
            GUILayout.Space(10);
            DrawDefaultInspector();
            GUILayout.Space(10);
            
            if(GUILayout.Button("Fill"))
            {
                var list = new List<GameObject>();
                FillList(GetPath(), 0, ref list);
                FillFields(list, prefabs.GetType());
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

        private void FillFields(IList<GameObject> list, Type type)
        {
            var fields = type.GetFields();
            
            foreach (var fieldInfo in fields)
            {
                for (var i = list.Count-1; i >= 0; i--)
                {
                    if (fieldInfo.Name != list[i].name) continue;
                    fieldInfo.SetValue(target,list[i]);
                    list.RemoveAt(i);
                }
            }
        }
    }
}