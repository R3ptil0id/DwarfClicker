using ScriptableObjects;
using UnityEditor;

namespace InspectorEditor
{
    [CustomEditor(typeof(WorkersPrefabs))]
    public class WorkersPrefabsEditor : BasePrefabsEditor<WorkersPrefabs>
    {
        protected override string GetPath()
        {
            return "Assets/Prefabs";
        }
    }
}