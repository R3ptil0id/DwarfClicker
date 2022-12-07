using System;
using System.Collections.Generic;

namespace Utils
{
    public static class EnumExtension
    {
        public static T GetItemByIndex<T>(int index)
        {
            var array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(index);
        }
        
        public static int GetItemIndex<T>(this Enum value)
        {
            var array = Enum.GetValues(typeof(T));
            for (var i = 0; i < array.Length; ++i)
            {
                if (Equals(array.GetValue(i), value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }
        
        public static IEnumerable<T> GetAllItems<T>() where T: Enum 
        {
            foreach (var item in Enum.GetValues(typeof(T)))
            {
                if (item.ToString().Contains("Undefined"))
                {
                    continue;
                }

                yield return (T)item;
            }
        }
    }
}