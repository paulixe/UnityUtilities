using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using Utilities;
using static UnityEditor.PlayerSettings;

namespace EditorUtilities
{

    [CustomPropertyDrawer(typeof(SerializedType))]
    public class SerializedTypeEditor : PropertyDrawer
    {
        private Assembly assembly;
        private bool foldout;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int lineIndex = 0;

            var serializedStringTypeProp = property.FindPropertyRelative("typeNameSerialized");
            var type = Type.GetType(serializedStringTypeProp.stringValue);
            if (type != null)
                assembly = Assembly.GetAssembly(type);
            var assemblyNameDisplay = assembly == null ? "choose assembly" : assembly.GetName().Name;

            foldout = EditorGUI.BeginFoldoutHeaderGroup(GetRect(position, ref lineIndex), foldout, label);
            if (foldout)
            {
                EditorGUI.indentLevel++;
                EditorGUI.BeginProperty(position, new GUIContent("serialized type"), serializedStringTypeProp);
                ;

                Rect assemblyRect = GetRect(position, ref lineIndex);
                Rect pos = EditorGUI.PrefixLabel(assemblyRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("assembly"));
                if (EditorGUI.DropdownButton(pos, new GUIContent(assemblyNameDisplay), FocusType.Keyboard))
                {
                    GenericMenu menu = new GenericMenu();
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

                    List<MenuItem> menuItems = new List<MenuItem>();
                    foreach (var newAssembly in assemblies)
                    {
                        if (newAssembly.GetTypes().Length == 0) continue;
                        string assemblyName = newAssembly.GetName().Name;
                        menu.AddItem(new GUIContent(assemblyName), false, () =>
                        {
                            assembly = newAssembly;
                            serializedStringTypeProp.stringValue = "";
                            property.serializedObject.ApplyModifiedProperties();
                        });

                    }
                    menu.ShowAsContext();
                }
                if (assembly != null)
                {
                    string typeNameDisplay = type == null ? "choose type" : type.Name;

                    Rect typeRect = GetRect(position, ref lineIndex);
                    pos = EditorGUI.PrefixLabel(typeRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("type"));

                    if (EditorGUI.DropdownButton(pos, new GUIContent(typeNameDisplay), FocusType.Keyboard))
                    {
                        GenericMenu menu = new GenericMenu();
                        List<MenuItem> menuItems = new List<MenuItem>();
                        foreach (var typeChoice in assembly.GetTypes())
                        {
                            menu.AddItem(new GUIContent(typeChoice.Name), false, () =>
                            {
                                serializedStringTypeProp.stringValue = typeChoice.AssemblyQualifiedName;
                                property.serializedObject.ApplyModifiedProperties();
                            });
                        }
                        menu.ShowAsContext();

                    }
                }
                EditorGUI.EndFoldoutHeaderGroup();
                EditorGUI.EndProperty();
            }

        }
        private Rect GetRect(Rect baseRect, ref int lineIndex)
        {
            Vector2 size = new Vector2(baseRect.width, EditorGUIUtility.singleLineHeight);
            Vector2 position = baseRect.position + new Vector2(0, lineIndex++ * (EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight));
            Rect rectNoIndent = new Rect(position, size);
            return EditorGUI.IndentedRect(rectNoIndent);

        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            if (!foldout)
                return lineHeight;
            else
            {
                if (assembly != null)
                {
                    return 3 * lineHeight;
                }
                else
                    return 2 * lineHeight;

            }


        }
    }
}
