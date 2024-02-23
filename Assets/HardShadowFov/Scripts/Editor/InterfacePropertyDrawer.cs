using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace m8t
{
    [CustomPropertyDrawer(typeof(InterfaceField))]
    public class InterfacePropertyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                var requiredAttribute = this.attribute as InterfaceField;
                var type = requiredAttribute.requiredType;

                EditorGUI.BeginProperty(position, label, property);


                Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(GameObject), true);
                
                if (obj is GameObject)
                {
                    var component = obj.GetComponent(type);
                    if (component != null)
                    {
                        property.objectReferenceValue = component;
                    }
                    else
                    {
                        Debug.LogError("You should assign game object with " + type + " component!");
                    }
                }
                
                EditorGUI.EndProperty();
            }
            else
            {
                var previousColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.BeginProperty(position, label, property);
                EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type"));
                EditorGUI.EndProperty();
                GUI.color = previousColor;
            }
        }
    }
}

