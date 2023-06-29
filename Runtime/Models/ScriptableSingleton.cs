using UnityEngine;
namespace Utilities
{
    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {

                    T[] assets = Resources.LoadAll<T>("");
                    if (assets == null || assets.Length < 1)
                    {
                        throw new System.Exception("Could not find any scriptable object instances in the resources");
                    }
                    else if (assets.Length > 1)
                    {
                        Debug.LogWarning("Multiple instances of the scriptable object found in the resources");
                    }
                    instance = assets[0];
                }
                return instance;
            }
        }
    }
}