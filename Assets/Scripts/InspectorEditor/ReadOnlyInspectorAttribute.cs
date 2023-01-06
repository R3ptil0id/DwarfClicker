using System;
using UnityEngine;

namespace InspectorEditor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyInspectorAttribute : PropertyAttribute { }
}