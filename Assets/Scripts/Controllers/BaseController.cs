using System.Linq;
using System.Reflection;
using Utils.Ioc;

namespace Controllers
{
    public abstract class BaseController
    {
        protected BaseController()
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