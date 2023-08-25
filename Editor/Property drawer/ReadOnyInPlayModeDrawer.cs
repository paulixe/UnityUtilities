using UnityEditor;
using UnityEngine;
using Utilities;

namespace EditorUtilities
{
    [CustomPropertyDrawer(typeof(ReadOnlyInPlayModeAttribute))]
    public class ReadOnyInPlayModeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            if (Application.isPlaying)
                GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;

            EditorGUI.EndProperty();
        }

    }
}
