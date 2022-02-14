using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;
using Week05.NumberGeneration;
using Week05.Variables;

namespace Week05
{
    [DisallowMultipleComponent]
    public class GameController : MonoBehaviour
    {
        private const int NUM_SAMPLES_MIN = 100;
        private const int NUM_SAMPLES_MAX = 10000;
        private const float LAMBDA_MIN = 0.1f;
        private const float LAMBDA_MAX = 10.0f;

        [System.Serializable]
        private class NumberGenerationEvent : UnityEvent<float> {}

        [Header("UI")]
        [SerializeField]
        private Text title;

        [Header("Variable Settings")]
        [SerializeField]
        [Label("Page")]
        private GameObject variableSettingsPage;

        [SerializeField]
        private DistributionMapper[] distributions;

        [SerializeField]
        private Dropdown distributionDropdown;

        [SerializeField]
        [Range(NUM_SAMPLES_MIN, NUM_SAMPLES_MAX)]
        [Label("# of Samples")]
        private int numSamples = 500;

        [SerializeField]
        private IntegerVariableSlider numSamplesSlider;

        [SerializeField]
        [Range(LAMBDA_MIN, LAMBDA_MAX)]
        private float lambda = 1.0f;

        [SerializeField]
        private FloatVariableSlider lambdaSlider;

        [Header("Results")]
        [SerializeField]
        [Label("Page")]
        private GameObject resultsPage;

        [SerializeField]
        [Range(1, 1000)]
        private int generationSpeed = 10;

        [SerializeField]
        private NumberGenerationEvent onNumberGenerated;

        #region Properties

        public int NumSamples
        {
            get => numSamples;
            set => numSamples = value;
        }

        public float Lambda
        {
            get => lambda;
            set => lambda = value;
        }

        public int GenerationSpeed
        {
            get => generationSpeed;
            set => generationSpeed = value;
        }

        public int DistributionIndex { get; set; }

        private float GenerationInterval => 1.0f / generationSpeed;

        #endregion // Properties

        void Start()
        {
            variableSettingsPage.SetActive(true);
            resultsPage.SetActive(false);

            SyncVarSettings();
        }

        public void StartGeneration()
        {
            DistributionMapper dist = distributions[distributionDropdown.value];
            int numS = numSamplesSlider.Value;
            float lam = lambdaSlider.Value;
            Debug.Log($"Generate {dist.GetName().ToLower()} distribution with lambda {lam} and {numS} samples");
        }

        private void SyncVarSettings()
        {
            distributionDropdown.options.Clear();
            distributionDropdown.options.AddRange(distributions.Select(d => new Dropdown.OptionData(d.GetName())));
            distributionDropdown.RefreshShownValue();

            numSamplesSlider.Value = numSamples;
            numSamplesSlider.Min = NUM_SAMPLES_MIN;
            numSamplesSlider.Max = NUM_SAMPLES_MAX;

            lambdaSlider.Value = lambda;
            lambdaSlider.Min = LAMBDA_MIN;
            lambdaSlider.Max = LAMBDA_MAX;
        }
    }
}
