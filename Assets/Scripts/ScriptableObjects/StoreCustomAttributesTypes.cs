using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Ioc;

namespace ScriptableObjects
{
    [RegistrateScriptableObjectInIoc]
    public class StoreCustomAttributesTypes : ScriptableObject
    {
        public List<Type> Types = new List<Type>();
    }
}