using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace BW.GameCode.Foundation
{

    /// <summary>
    /// ScriptableObject µ¥Àý
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        static T instance = null;
        public static T I {
            get {
                if (!instance)
                    instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                Debug.Assert(instance != null, $"{typeof(T).Name} IS NULL!;");
                return instance;
            }
        }
    }
}