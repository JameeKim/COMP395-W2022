using UnityEngine;

namespace Week05.NumberGeneration
{
    [CreateAssetMenu(fileName = "ExponentialDistribution", menuName = "Distribution/Exponential", order = 0)]
    public class ExponentialMapper : DistributionMapper
    {
        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float meanMultiplierForRange = 5.0f;

        public override string GetName()
        {
            return "Exponential";
        }

        public override float ApplyMapping(float uniformRandom)
        {
            return -1.0f * Mathf.Log(1.0f - uniformRandom) / Config.lambda;
        }

        public override Vector2 GetRange()
        {
            return new Vector2(0.0f, GetTheoreticalMean() * meanMultiplierForRange);
        }

        public override float GetTheoreticalMean()
        {
            return 1.0f / Config.lambda;
        }

        public override Vector2Int GetMaxAndIncrementForGraphBar()
        {
            float probabilityForFirstBin = 1.0f - Mathf.Exp(-1.0f * meanMultiplierForRange / Config.numBins);
            int initialMax = (int) (probabilityForFirstBin * Config.numSamples);
            return GetMaxAndIncrementForGraphBarFromInitialMax(initialMax);
        }
    }
}
