using System;

namespace Utils.Ioc
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class RegistrateMonoBehaviourInIocAttribute : InitializableAttribute{
        public RegistrateMonoBehaviourInIocAttribute(bool needInitialize = false) : base(needInitialize)
        {
        }
    }
}