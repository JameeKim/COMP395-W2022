using UnityEngine;
using UnityEngine.UI;

namespace Week05
{
    [DisallowMultipleComponent]
    public class GraphBar : MonoBehaviour
    {
        [SerializeField]
        private Text bin;

        [SerializeField]
        private RectTransform barFill;

        [SerializeField]
        private RectTransform barValue;

        [SerializeField]
        private Text barValueText;

        private int maxValue;

        private int currentValue;

        private float rangeMin;

        private float rangeMax;

        private float Percentage => maxValue > 0 ? (float) currentValue / maxValue : 0.0f;

        public void SetRange(float min, float length)
        {
            rangeMin = min;
            rangeMax = min + length;
            bin.text = rangeMax.ToString("F");
        }

        public void SetRim(float oneRangeValue, bool isOnRightSide)
        {
            rangeMin = isOnRightSide ? oneRangeValue : float.MinValue;
            rangeMax = isOnRightSide ? float.MaxValue : oneRangeValue;
            bin.text = isOnRightSide ? "Inf" : oneRangeValue.ToString("F");
        }

        public void IncreaseMax(int amount)
        {
            maxValue += amount;
            UpdateBar();
        }

        public bool IsInRange(float value)
        {
            return rangeMin <= value && value < rangeMax;
        }

        // Returns true if current value reached the max value
        public bool IncreaseByOne()
        {
            currentValue += 1;
            UpdateBar();
            return currentValue >= maxValue;
        }

        private void UpdateBar()
        {
            barFill.anchorMax = new Vector2(1.0f, Percentage);
            barValue.anchorMin = new Vector2(0.0f, Percentage);
            barValue.anchorMax = new Vector2(1.0f, Percentage);
            barValueText.text = currentValue.ToString();
        }
    }
}
