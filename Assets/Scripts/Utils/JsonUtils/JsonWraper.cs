using System;

namespace Utils.JsonUtils
{
    [Serializable]
    public class JsonWraper<T>
    {
        public T[] DataArray;
    }
}