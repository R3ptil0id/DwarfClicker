using System.Collections.Generic;
using InspectorEditor;
using UnityEngine;

namespace Utils.Ioc
{
    public class MonoBehaviourIocInstaller : MonoBehaviour
    {
        [ReadOnlyInspector] public List<MonoBehaviour> GameObjects = new ();
        [ReadOnlyInspector] public List<MonoBehaviour> Initializables = new ();
    }
}