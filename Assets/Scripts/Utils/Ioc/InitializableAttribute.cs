using System;

namespace Utils.Ioc
{
    public abstract class InitializableAttribute : Attribute
    {
        public bool NeedInitialize { get; private set; }
        public InitializableAttribute(bool needInitialize)
        {
            NeedInitialize = needInitialize;
        }
    }
}