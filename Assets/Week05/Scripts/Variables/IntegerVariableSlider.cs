using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Week05.Variables
{
    [DisallowMultipleComponent]
    public class IntegerVariableSlider : MonoBehaviour
    {
        [System.Serializable]
        public class IntegerVariableChangeEvent : UnityEvent<int> {}

        [Header("Variable")]
        [SerializeField]
        [Label("Name")]
        private string variableName = "Integer Variable";

        [SerializeField]
        [DynamicRange("Min", "Max")]
        private int value = 1;

        [SerializeField]
        private IntegerVariableChangeEvent onValueChanged;

        [Header("Object References")]
        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Vector2Int valueMinMax = new(1, 10000);

        [SerializeField]
        private Slider valueSlider;

        [SerializeField]
        private InputField valueInput;

        public int Value
        {
            get => value;
            set
            {
                int previousValue = this.value;
                this.value = Clamp(value);
                valueSlider.SetValueWithoutNotify(this.value);
                SetInputToCurrent();
                if (previousValue != this.value)
                    onValueChanged.Invoke(this.value);
            }
        }

        public int Min
        {
            get => valueMinMax.x;
            set
            {
                valueSlider.minValue = value;
                valueMinMax.x = (int) valueSlider.minValue;
            }
        }

        public int Max
        {
            get => valueMinMax.y;
            set
            {
                valueSlider.maxValue = value;
                valueMinMax.y = (int) valueSlider.maxValue;
                valueInput.characterLimit = valueMinMax.y.ToString().Length;
            }
        }

        void Start()
        {
            nameText.text = variableName;

            valueSlider.wholeNumbers = true;
            valueSlider.minValue = Min;
            valueSlider.maxValue = Max;
            value = (int) valueSlider.value;

            valueInput.characterLimit = Max.ToString().Length;
            valueInput.characterValidation = InputField.CharacterValidation.Integer;
            SetInputToCurrent();

            onValueChanged.Invoke(Value);
        }

        public void SetValue(float floatValue)
        {
            Value = (int) floatValue;
        }

        public void SetValue(string stringValue)
        {
            try
            {
                Value = System.Convert.ToInt32(stringValue);
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

        private int Clamp(int input) => Mathf.Clamp(input, Min, Max);

        private void SetInputToCurrent()
        {
            valueInput.SetTextWithoutNotify(Value.ToString());
        }
    }
}
