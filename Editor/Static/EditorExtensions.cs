using System;
using UnityEditor;

namespace EditorUtilities
{
    public static class EditorExtensions
    {
        public static T GetEnumValue<T>(this SerializedProperty serializedProperty) where T : Enum
        {
            return (T)Enum.GetValues(typeof(T)).GetValue(serializedProperty.enumValueIndex);
        }
    }
}
