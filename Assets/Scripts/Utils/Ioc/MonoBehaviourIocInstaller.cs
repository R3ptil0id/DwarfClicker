using System.Collections.Generic;
using UnityEngine;

namespace Utils.Ioc
{
    public class MonoBehaviourIocInstaller : MonoBehaviour
    {
        [HideInInspector] public List<MonoBehaviour> GameObjects = new ();
        [HideInInspector] public List<MonoBehaviour> Initializables = new ();
    }
}