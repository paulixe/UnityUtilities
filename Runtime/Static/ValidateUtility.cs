using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Obj = UnityEngine.Object;
namespace Historisation
{
    public static class ValidateUtility
    {
        public static bool CheckEnumerableValues(Obj thisObject, string fieldName, IEnumerable enumerableObjectToCheck)
        {
            bool error = false;
            int count = 0;
            foreach (var item in enumerableObjectToCheck)
            {
                if (item == null)
                {
                    Debug.Log(fieldName + " has null values in object " + thisObject.name);
                    error = true;
                }
                else
                {
                    count++;
                }
            }
            if (count == 0)
            {
                Debug.Log(fieldName + " has no values in object " + thisObject.name);
                error = true;
            }
            return error;
        }
        public static void CheckEmptyString(Obj thisObject, string fieldName, string value)
        {
            if (value == null || value.Length == 0)
            {
                Debug.LogWarning($"The field {fieldName} is null or empty in the object {thisObject.name}", thisObject);
            }
        }
        public static void CheckNullObject(Obj thisObject, string fieldName, Obj value)
        {
            if (value == null)
            {
                Debug.LogWarning($"The field {fieldName} is null in the object {thisObject.name}", thisObject);
            }
        }
        public static void CheckUniqueList<T>(Obj thisObject, string listName, IEnumerable<T> list, Func<T, T, bool> equalizer)
        {
            T[] array = list.ToArray();
            for (int i = 0; i < array.Length; i++)
                for (int j = i + 1; j < array.Length; j++)
                    if (equalizer(array[i], array[j]))
                        Debug.LogWarning($"They are alike fields in the list {listName} of the object {thisObject.name}", thisObject);
        }
        public static void CheckList<T>(Obj thisObject, string listName, IEnumerable<T> list, Func<T, bool> validater)
        {
            T[] array = list.ToArray();
            for (int i = 0; i < array.Length; i++)
                if (!validater(array[i]))
                    Debug.LogWarning($"They are unvalidate fields in the list {listName} of the object {thisObject.name}", thisObject);
        }
    }
}
