using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace LevelGenerator
{
    public class SpaceBodyRandomizer
    {
        private const float NOISE = 7.5f;
        
        private SpaceBodyReferences _spaceBodyReferences;

        private Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyWeight> _spaceBodyWeights =
            new Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyWeight>
            {
                {SpaceBodyControllerBase.SpaceBodyType.BlackHole, new SpaceBodyWeight(5f)},
                {SpaceBodyControllerBase.SpaceBodyType.Star, new SpaceBodyWeight(10f)},
                {SpaceBodyControllerBase.SpaceBodyType.Asteroid, new SpaceBodyWeight(15f)},
                {SpaceBodyControllerBase.SpaceBodyType.Planet, new SpaceBodyWeight(20f)},
            };

        private int _numberInstances = 0;
        public SpaceBodyRandomizer(SpaceBodyReferences spaceBodyReferences, int numberInstances)
        {
            _spaceBodyReferences = spaceBodyReferences;
            _numberInstances = numberInstances;
        }
        
        public SpaceBodyControllerBase.SpaceBodyType GetRandomSpaceBody()
        {
            SpaceBodyControllerBase.SpaceBodyType mostWeightOffset = SpaceBodyControllerBase.SpaceBodyType.Planet;
            float maxWeightOffset = 0f;
            
            foreach (KeyValuePair<SpaceBodyControllerBase.SpaceBodyType,SpaceBodyWeight> spaceBodyWeight in _spaceBodyWeights)
            {
                float offset = spaceBodyWeight.Value.WeightOffset + GetNoise();
                if (offset > maxWeightOffset)
                {
                    maxWeightOffset = offset;
                    mostWeightOffset = spaceBodyWeight.Key;
                }
            }

            _spaceBodyWeights[mostWeightOffset].NumberInstances++;
            foreach (SpaceBodyWeight spaceBodyWeight in _spaceBodyWeights.Values)
            {
                spaceBodyWeight.UpdateWeight(_numberInstances);
            }
            return mostWeightOffset;
        }

        public void Remove(SpaceBodyControllerBase.SpaceBodyType spaceBodyType)
        {
            _spaceBodyWeights[spaceBodyType].NumberInstances--;
            _spaceBodyWeights[spaceBodyType].UpdateWeight(_numberInstances);
        }

        private float GetNoise()
        {
            return Random.Range(-NOISE, NOISE);
        }
    }
}