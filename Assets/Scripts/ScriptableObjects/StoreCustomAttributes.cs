using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [RegistrateScriptableObjectInIoc]
    public class StoreCustomAttributes : ScriptableObject
    {
        public List<Type> Types = new List<Type>();
        public List<Type> NeedInitializeTypes = new List<Type>();
    }
}