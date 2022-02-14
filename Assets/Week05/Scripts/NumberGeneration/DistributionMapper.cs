using UnityEngine;

namespace Week05.NumberGeneration
{
    public abstract class DistributionMapper : ScriptableObject
    {
        public DistributionConfig Config { get; set; }

        public abstract string GetName();

        public abstract float ApplyMapping(float uniformRandom);

        public abstract Vector2 GetRange();

        public abstract float GetTheoreticalMean();

        public abstract Vector2Int GetMaxAndIncrementForGraphBar();

        protected Vector2Int GetMaxAndIncrementForGraphBarFromInitialMax(int initialMax)
        {
            int increment = initialMax / 10;
            return new Vector2Int(initialMax + increment, increment);
        }
    }
}
