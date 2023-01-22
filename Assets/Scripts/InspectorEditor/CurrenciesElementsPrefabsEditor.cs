using ScriptableObjects;
using UnityEditor;

namespace InspectorEditor
{
    [CustomEditor(typeof(CurrenciesElementsPrefabs))]
    public class CurrenciesElementsPrefabsEditor : BasePrefabsEditor<CurrenciesElementsPrefabs>
    {
        protected override string GetPath()
        {
            return "Assets/Prefabs/Currencies";
        }
    }
}