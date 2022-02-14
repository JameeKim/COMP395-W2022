using UnityEngine;

namespace Week05.NumberGeneration
{
    public abstract class DistributionMapper : ScriptableObject
    {
        public abstract string GetName();

        public abstract float ApplyMapping(float uniformRandom);
    }
}
