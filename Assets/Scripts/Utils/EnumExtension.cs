using System;
using System.Collections.Generic;

namespace Utils
{
    public static class EnumExtension
    {
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
                yield return (T)item;
            }
        }
    }
}