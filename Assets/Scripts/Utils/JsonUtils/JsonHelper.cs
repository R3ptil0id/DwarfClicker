using System.Collections.Generic;
using UnityEngine;

namespace Utils.JsonUtils
{
    public class JsonHelper
    {
        public static T[] GetData<T>(string path)
        {
            var targetFile = Resources.Load<TextAsset>(path);
            var perks = JsonUtility.FromJson<JsonWraper<T>>(targetFile.text).DataArray;
            
            return perks;
        }
    }
}