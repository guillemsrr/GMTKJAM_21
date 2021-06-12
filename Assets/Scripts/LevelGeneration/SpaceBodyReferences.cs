using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace LevelGenerator
{
    [CreateAssetMenu(fileName = "SpaceBodyReferences", menuName = "ScriptableObjects/SpaceBodyReferences")]
    public class SpaceBodyReferences: ScriptableObject
    {
        [SerializeField] private Planet _planet;
        [SerializeField] private Asteroid _asteroid;

        private Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyControllerBase> _spaceBodiesDictionary;


        public void Initialize()
        {
            _spaceBodiesDictionary = new Dictionary<SpaceBodyControllerBase.SpaceBodyType, SpaceBodyControllerBase>
            {
                {SpaceBodyControllerBase.SpaceBodyType.Asteroid, _asteroid},
                {SpaceBodyControllerBase.SpaceBodyType.Planet, _planet},
            };
        }

        public SpaceBodyControllerBase GetSpaceBody(SpaceBodyControllerBase.SpaceBodyType type)
        {
            return _spaceBodiesDictionary[type];
        }
    }
}