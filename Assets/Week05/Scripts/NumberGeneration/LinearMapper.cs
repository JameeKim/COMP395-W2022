using UnityEngine;

namespace Week05.NumberGeneration
{
    [CreateAssetMenu(fileName = "UniformDistribution", menuName = "Distribution/Uniform")]
    public class LinearMapper : DistributionMapper
    {
        public override string GetName()
        {
            return "Uniform Constant";
        }

        public override float ApplyMapping(float uniformRandom)
        {
            return uniformRandom;
        }

        public override Vector2 GetRange()
        {
            return new Vector2(0.0f, 1.0f);
        }

        public override float GetTheoreticalMean()
        {
            return 0.5f;
        }

        public override Vector2Int GetMaxAndIncrementForGraphBar() =>
            GetMaxAndIncrementForGraphBarFromInitialMax(Config.numSamples / Config.numBins);
    }
}
