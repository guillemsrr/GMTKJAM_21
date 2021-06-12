namespace LevelGenerator
{
    public class SpaceBodyWeight
    {
        public float _weight;
        private float _targetWeight;
        public int NumberInstances;

        public float WeightOffset => _targetWeight - _weight;

        public SpaceBodyWeight(float targetWeight)
        {
            _weight = 0f;
            NumberInstances = 0;
            _targetWeight = targetWeight;
        }

        public void UpdateWeight(int totalInstances)
        {
            _weight = (NumberInstances / totalInstances) * 100f;
        }
    }
}