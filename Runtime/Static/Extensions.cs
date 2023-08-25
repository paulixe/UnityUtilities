using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Extensions
    {
        public static T[] Populate<T>(this T[] collection) where T : new()
        {
            for (int i = 0; i < collection.Length; i++)
                collection[i] = new T();
            return collection;
        }
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
