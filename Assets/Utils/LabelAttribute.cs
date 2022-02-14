using UnityEngine;

namespace Utils
{
    public class LabelAttribute : PropertyAttribute
    {
        public readonly string Label;

        public LabelAttribute(string label) => Label = label;
    }
}
