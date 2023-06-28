using System;
using UnityEngine;
namespace Historisation
{
    public class MonobehaviourSingleton<T> : MonoBehaviour where T : MonobehaviourSingleton<T>
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                throw new Exception($"You can't have several singletons in one scene, {typeof(T).Name} in {gameObject.name} is destroyed");
            }
            else
            {
                Instance = this as T;
            }

        }
    }
}
