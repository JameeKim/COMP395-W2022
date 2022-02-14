using UnityEngine;
using UnityEngine.UI;
using Week05.NumberGeneration;

namespace Week05
{
    public class ResultsPage : MonoBehaviour
    {
        [SerializeField]
        private Transform graphArea;

        [SerializeField]
        private GameObject graphBarPrefab;

        [SerializeField]
        private Text averageText;

        [SerializeField]
        private Text theoreticalAverageText;

        [SerializeField]
        private Button backButton;

        private GraphBar[] graphBars;

        private int receivedSamples;

        private int increment;

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

        public void SetUp(DistributionMapper dist)
        {
            Vector2 range = dist.GetRange();
            int numBins = dist.Config.numBins;
            theoreticalAverageText.text = dist.GetTheoreticalMean().ToString("F3");
            Vector2Int maxAndIncrement = dist.GetMaxAndIncrementForGraphBar();
            increment = maxAndIncrement.y;

            int initialMax = maxAndIncrement.x;
            float binLength = (range.y - range.x) / numBins;

            graphBars = new GraphBar[numBins + 2];
            graphBars[0] = CreateNewBar(range.x, -1.0f, initialMax);

            for (int i = 0; i < numBins; i++)
            {
                graphBars[i + 1] = CreateNewBar(range.x + binLength * i, binLength, initialMax);
            }

            graphBars[numBins + 1] = CreateNewBar(range.y, 0.0f, initialMax);
        }

        public void OnNumberReceived(float number)
        {
            total += number;
            receivedSamples += 1;

            if (needMoreSpace)
            {
                foreach (GraphBar bar in graphBars)
                {
                    bar.IncreaseMax(increment);
                }

                needMoreSpace = false;
            }

            foreach (GraphBar bar in graphBars)
            {
                if (bar.IsInRange(number))
                {
                    needMoreSpace |= bar.IncreaseByOne();
                    break;
                }
            }

            averageText.text = (total / receivedSamples).ToString("F3");
        }

        public void OnGenerationFinished()
        {
            backButton.interactable = true;
        }

        private void ResetPage()
        {
            foreach (GraphBar bar in graphBars)
            {
                Destroy(bar.gameObject);
            }

            graphBars = null;

            averageText.text = "0.000";
            theoreticalAverageText.text = "0.000";
            backButton.interactable = false;
            receivedSamples = 0;
            increment = 0;
            total = 0.0f;
            needMoreSpace = false;
        }

        private GraphBar CreateNewBar(float min, float length, int initialMax)
        {
            GameObject barObj = Instantiate(graphBarPrefab, graphArea);
            GraphBar bar = barObj.GetComponent<GraphBar>();
            bar.IncreaseMax(initialMax);

            switch (length)
            {
                case > 0.0f: // regular bin
                    bar.SetRange(min, length);
                    break;
                case <= -1.0f: // left side
                    bar.SetRim(min, false);
                    break;
                default: // right side
                    bar.SetRim(min, true);
                    break;
            }

            return bar;
        }
    }
}
