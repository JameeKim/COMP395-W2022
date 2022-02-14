using UnityEngine;

namespace Utils
{
    public class DynamicRangeAttribute : PropertyAttribute
    {
        public readonly string Min;
        public readonly string Max;

        public DynamicRangeAttribute(string min, string max)
        {
            Min = min;
            Max = max;
        }
    }
}
