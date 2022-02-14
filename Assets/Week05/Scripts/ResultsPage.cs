using UnityEngine;
using UnityEngine.UI;
using Utils;
using Week05.NumberGeneration;

namespace Week05
{
    public class ResultsPage : MonoBehaviour
    {
        [SerializeField]
        [Range(5, 20)]
        [Label("# of Bins")]
        private int numBins = 10;

        [SerializeField]
        private Transform graphArea;

        [SerializeField]
        private GameObject graphBarPrefab;

        [SerializeField]
        private Button backButton;

        private Vector2 rangeX;

        private GraphBar[] graphBars;

        private int numSamples;

        private int initialMax;

        private float total;

        private bool needMoreSpace;

        void OnEnable()
        {
            backButton.interactable = false;
        }

        void OnDisable()
        {
            if (graphBars != null)
            {
                ResetPage();
            }
        }

        public void SetUp(DistributionMapper dist, int inNumSamples)
        {
            rangeX = dist.GetRange();
            numSamples = inNumSamples;

            initialMax = Mathf.Max(numSamples / numBins, 10);
            initialMax -= initialMax % 10;
            float binLength = (rangeX.y - rangeX.x) / numBins;
            graphBars = new GraphBar[numBins];

            for (int i = 0; i < numBins; i++)
            {
                GameObject barObj = Instantiate(graphBarPrefab, graphArea);
                GraphBar bar = barObj.GetComponent<GraphBar>();
                bar.SetRange(rangeX.x + binLength * i, binLength);
                bar.IncreaseMax(initialMax);
                graphBars[i] = bar;
            }
        }

        public void OnNumberReceived(float number)
        {
            total += number;

            if (number < rangeX.x || rangeX.y <= number)
            {
                Debug.Log($"Number {number} out of range");
                return;
            }

            if (needMoreSpace)
            {
                int increase = Mathf.Max(initialMax / 10, 10);
                increase -= increase % 10;

                foreach (GraphBar bar in graphBars)
                {
                    bar.IncreaseMax(increase);
                }

                needMoreSpace = false;
            }

            foreach (GraphBar bar in graphBars)
            {
                if (bar.IsInRange(number))
                {
                    needMoreSpace |= bar.IncreaseByOne();
                }
            }
        }

        public void OnGenerationFinished()
        {
            Debug.Log($"Average: {total / numSamples}");
            backButton.interactable = true;
        }

        private void ResetPage()
        {
            foreach (GraphBar bar in graphBars)
            {
                Destroy(bar.gameObject);
            }

            graphBars = null;

            backButton.interactable = false;
            rangeX = Vector2.zero;
            numSamples = 0;
            initialMax = 0;
            total = 0.0f;
            needMoreSpace = false;
        }
    }
}
