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
                var o = IoC.Resolve(fieldInfo.FieldType);
                fieldInfo.SetValue(this, o);
            }
        }
    }
}