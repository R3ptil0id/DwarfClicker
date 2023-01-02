using System;

namespace Utils.Ioc
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RegistrateInIocAttribute : InitializableAttribute
    {
        public RegistrateInIocAttribute(bool needInitialize = false) : base(needInitialize)
        {
        }
    }
}