using System.Linq;
using System.Reflection;
using UnityEngine;
using Utils.Ioc;

namespace Controls
{
    public class BaseControl : MonoBehaviour, IInitializable
    {
        public void Initialize()
        {
            var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldInfos = fields.Where(f => f.IsDefined(typeof(InjectAttribute))).ToList();

            foreach (var fieldInfo in fieldInfos)
            {
                var f = fieldInfo.FieldType;
                var t = IoC.Resolve(fieldInfo.FieldType);
                fieldInfo.SetValue(this, t);
            }
        }
    }
}