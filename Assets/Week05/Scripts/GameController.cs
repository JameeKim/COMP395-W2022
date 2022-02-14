using System.Collections;
using System.Linq;
using UnityEngine;
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
        private const int NUM_SAMPLES_MAX = 100000;
        private const int NUM_BINS_MIN = 5;
        private const int NUM_BINS_MAX = 30;
        private const float LAMBDA_MIN = 0.1f;
        private const float LAMBDA_MAX = 10.0f;

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
        private int numSamples = 1000;

        [SerializeField]
        private IntegerVariableSlider numSamplesSlider;

        [SerializeField]
        [Range(NUM_BINS_MIN, NUM_BINS_MAX)]
        [Label("# of Bins")]
        private int numBins = 20;

        [SerializeField]
        private IntegerVariableSlider numBinsSlider;

        [SerializeField]
        [Range(LAMBDA_MIN, LAMBDA_MAX)]
        private float lambda = 1.0f;

        [SerializeField]
        private FloatVariableSlider lambdaSlider;

        [Header("Results")]
        [SerializeField]
        [Label("Page")]
        private ResultsPage resultsPage;

        [SerializeField]
        [Range(1, 1000)]
        private int maxBatch = 100;

        void Start()
        {
            variableSettingsPage.SetActive(true);
            resultsPage.gameObject.SetActive(false);

            SyncVarSettings();
        }

        public void StartGeneration()
        {
            DistributionMapper dist = distributions[distributionDropdown.value];
            int numS = numSamplesSlider.Value;
            int numB = numBinsSlider.Value;
            float lam = lambdaSlider.Value;

            variableSettingsPage.SetActive(false);
            resultsPage.gameObject.SetActive(true);

            DistributionConfig config = new DistributionConfig { numSamples = numS, numBins = numB, lambda = lam };
            StartCoroutine(DoGeneration(dist, config));
        }

        public void BackToSettings()
        {
            variableSettingsPage.SetActive(true);
            resultsPage.gameObject.SetActive(false);
        }

        private void SyncVarSettings()
        {
            distributionDropdown.options.Clear();
            distributionDropdown.options.AddRange(distributions.Select(d => new Dropdown.OptionData(d.GetName())));
            distributionDropdown.RefreshShownValue();

            numSamplesSlider.Value = numSamples;
            numSamplesSlider.Min = NUM_SAMPLES_MIN;
            numSamplesSlider.Max = NUM_SAMPLES_MAX;

            numBinsSlider.Value = numBins;
            numBinsSlider.Min = NUM_BINS_MIN;
            numBinsSlider.Max = NUM_BINS_MAX;

            lambdaSlider.Value = lambda;
            lambdaSlider.Min = LAMBDA_MIN;
            lambdaSlider.Max = LAMBDA_MAX;
        }

        private void OnGenerationFinished()
        {
            resultsPage.OnGenerationFinished();
        }

        private IEnumerator DoGeneration(DistributionMapper dist, DistributionConfig config)
        {
            dist.Config = config;
            resultsPage.SetUp(dist);

            yield return null;

            int numSample = config.numSamples;

            while (true)
            {
                int limit = Mathf.Min(numSample, maxBatch);
                int i = 0;

                while (i < limit)
                {
                    float rand = Random.value;
                    float mapped = dist.ApplyMapping(rand);
                    resultsPage.OnNumberReceived(mapped);
                    i++;
                }

                numSample -= i;

                if (numSample > 0)
                {
                    yield return null;
                }
                else
                {
                    break;
                }
            }

            OnGenerationFinished();
        }
    }
}
