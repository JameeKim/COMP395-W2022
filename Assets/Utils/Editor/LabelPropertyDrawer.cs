using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    public class LabelPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SetNewNameForLabel(label);
            EditorGUI.PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SetNewNameForLabel(label);
            return base.GetPropertyHeight(property, label);
        }

        private void SetNewNameForLabel(GUIContent label)
        {
            label.text = ((LabelAttribute) attribute).Label;
        }
    }
}
