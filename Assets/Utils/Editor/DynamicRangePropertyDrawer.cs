using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    [CustomPropertyDrawer(typeof(DynamicRangeAttribute))]
    public class DynamicRangePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            DynamicRangeAttribute attr = (DynamicRangeAttribute) attribute;
            Object obj = property.serializedObject.targetObject;
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            object min = obj.GetType().GetProperty(attr.Min, flags)?.GetValue(obj);
            object max = obj.GetType().GetProperty(attr.Max, flags)?.GetValue(obj);

            if (min == null || max == null)
            {
                EditorGUI.LabelField(
                    position,
                    label,
                    new GUIContent($"Failed to find property {attr.Min} or {attr.Max}")
                );
                return;
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                    EditorGUI.Slider(position, property, (float) min, (float) max, label);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUI.IntSlider(position, property, (int) min, (int) max, label);
                    break;
                default:
                    GUIStyle style = new GUIStyle { margin = { top = 5, bottom = 5 } };
                    EditorGUI.LabelField(
                        position,
                        label,
                   new GUIContent("DynamicRange should be used on float or int"),
                        style
                    );
                    break;
            }
        }
    }
}
