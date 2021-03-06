using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChopChop
{
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T[] assets = Resources.LoadAll<T>("Config");
                    if (assets == null || assets.Length < 1)
                    {
                        Debug.LogError($"{typeof(T)}: assets Length is less than one.");
                        return null;
                    }
                    else if (assets.Length > 1)
                    {
                        Debug.LogWarning($"{typeof(T)}: Multiple instances is founded in the Recources. Returned first founded.");
                    }

                    _instance = assets[0];
                }

                return _instance;
            }
        }
    }
}