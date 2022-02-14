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
    }
}
