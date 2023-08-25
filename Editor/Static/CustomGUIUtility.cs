using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorUtilities
{
    public static class CustomGUIUtility
    {
        public static void ForceProjectWindowRepaint()
        {
            var projectBrowserType = typeof(Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
            var resetViewsMethod = projectBrowserType.GetMethod("ResetViews", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var window in Resources.FindObjectsOfTypeAll(projectBrowserType))
            {
                resetViewsMethod.Invoke(window, new object[0]);
            }
        }
        public static void DrawScriptLine(SerializedObject serializedObject)
        {
            GUI.enabled = false;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));
            GUI.enabled = true;
        }
        public static string FormatPropertyNameToBackingField(string propertyName)
        {
            return "<" + propertyName + ">k__BackingField";
        }
        public static object DrawObject(Rect position, string name, object value)
        {
            if (value == null)
            {
                EditorGUI.LabelField(position, "value is null");
                return null;
            }
            var type = value.GetType();

            if (type == typeof(string))
                return EditorGUI.TextField(position, new GUIContent(name), value.ToString());
            else if (type == typeof(int))
                return EditorGUI.IntField(position, new GUIContent(name), (int)value);
            else if (type == typeof(float))
                return EditorGUI.FloatField(position, new GUIContent(name), (float)value);
            else if (type == typeof(bool))
                return EditorGUI.Toggle(position, new GUIContent(name), (bool)value);
            else if (type == typeof(Color))
                return EditorGUI.ColorField(position, new GUIContent(name), (Color)value);
            else
            {
                EditorGUI.LabelField(position, "type not implemented");
                return null;
            }

        }
    }


}

