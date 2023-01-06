using System;

namespace Utils.Ioc
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public abstract class InitializableAttribute : Attribute
    {
        public bool NeedInitialize { get; private set; }
        public InitializableAttribute(bool needInitialize)
        {
            NeedInitialize = needInitialize;
        }
    }
}