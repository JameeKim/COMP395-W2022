using UnityEngine;

namespace Week05.NumberGeneration
{
    public abstract class DistributionMapper : ScriptableObject
    {
        public DistributionConfig Config { get; set; }

        public abstract string GetName();

        public abstract float ApplyMapping(float uniformRandom);

        public abstract Vector2 GetRange();
    }
}
