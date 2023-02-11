using System;
using System.Linq;
using System.Reflection;

namespace Utils.Ioc
{
    public static class Injecter
    {
        public static void Inject(this object obj)
        {
            RecursiveInjecting(obj.GetType(), obj);
        }

        private static void RecursiveInjecting(Type type, Object obj)
        {
            while (true)
            {
                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                var fieldInfos = fields.Where(f => f.IsDefined(typeof(InjectAttribute))).ToList();

                foreach (var fieldInfo in fieldInfos)
                {
                    var f = fieldInfo.FieldType;
                    var t = IoC.Resolve(fieldInfo.FieldType);
                    fieldInfo.SetValue(obj, t);
                }
                var baseType = type.BaseType;
                if (baseType != null && type != baseType)
                {
                    type = baseType;
                    continue;
                }

                break;
            }
        }
    }
}