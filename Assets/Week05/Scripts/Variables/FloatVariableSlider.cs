using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Week05.Variables
{
    [DisallowMultipleComponent]
    public class FloatVariableSlider : MonoBehaviour
    {
        [System.Serializable]
        public class FloatVariableChangeEvent : UnityEvent<float> {}

        [Header("Variable")]
        [SerializeField]
        [Label("Name")]
        private string variableName = "Float Variable";

        [SerializeField]
        private float value = 0.5f;

        [SerializeField]
        private FloatVariableChangeEvent onValueChanged;

        [Header("Object References")]
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Vector2 valueMinMax = new(0.0f, 1.0f);

        [SerializeField]
        private Slider valueSlider;

        [SerializeField]
        private InputField valueInput;

        public float Value
        {
            get => value;
            set
            {
                float previousValue = this.value;
                this.value = Clamp(value);
                valueSlider.SetValueWithoutNotify(this.value);
                SetInputToCurrent();
                if (Mathf.Abs(previousValue - this.value) > 1.0E-3)
                    onValueChanged.Invoke(this.value);
            }
        }

        public float Min
        {
            get => valueMinMax.x;
            set
            {
                valueSlider.minValue = value;
                valueMinMax.x = valueSlider.minValue;
            }
        }

        public float Max
        {
            get => valueMinMax.y;
            set
            {
                valueSlider.maxValue = value;
                valueMinMax.y = valueSlider.maxValue;
            }
        }

        void Start()
        {
            nameText.text = variableName;

            valueSlider.wholeNumbers = false;
            valueSlider.minValue = Min;
            valueSlider.maxValue = Max;
            value = valueSlider.value;

            valueInput.characterLimit = 0;
            valueInput.characterValidation = InputField.CharacterValidation.Decimal;
            SetInputToCurrent();

            onValueChanged.Invoke(Value);
        }

        public void SetValue(float floatValue)
        {
            Value = floatValue;
        }

        public void SetValue(string stringValue)
        {
            try
            {
                Value = System.Convert.ToSingle(stringValue);
            }
            catch (System.FormatException)
            {
                SetInputToCurrent();
            }
            catch (System.OverflowException)
            {
                SetInputToCurrent();
            }
        }

        private float Clamp(float input)
        {
            float clamped = Mathf.Clamp(input, Min, Max);
            return Mathf.Round(100.0f * clamped) / 100.0f;
        }

        private void SetInputToCurrent()
        {
            valueInput.SetTextWithoutNotify(Value.ToString("F2"));
        }
    }
}
