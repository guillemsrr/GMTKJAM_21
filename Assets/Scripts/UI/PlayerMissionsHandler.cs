using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class PlayerMissionsHandler: MonoBehaviour
    {
        private const int BASE_NUMBER_MISSIONS = 1;

        [SerializeField] private Transform _missionContainer;
        
        private List<SpaceBodyControllerBase.SpaceBodyType> _missionTypes;
        
        public void CreateMissions(int numberMissions)
        {
            numberMissions += BASE_NUMBER_MISSIONS;
            for (int i = 0; i < numberMissions; i++)
            {
                _missionTypes.Add(RandomEnum.GetRandomFromEnum<SpaceBodyControllerBase.SpaceBodyType>());
                
                //add into container
            }
        }

        public bool IsTypeInMission(SpaceBodyControllerBase.SpaceBodyType type)
        {
            return _missionTypes.Contains(type);
        }
    }
}