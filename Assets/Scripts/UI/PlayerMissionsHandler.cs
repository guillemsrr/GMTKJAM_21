using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class PlayerMissionsHandler: MonoBehaviour
    {
        private List<SpaceBodyControllerBase.SpaceBodyType> _missionTypes;
        
        public void CreateMissions(int numberMissions)
        {
            for (int i = 0; i < numberMissions; i++)
            {
                _missionTypes.Add(RandomEnum.GetRandomFromEnum<SpaceBodyControllerBase.SpaceBodyType>());
            }
        }

        public bool IsTypeInMission(SpaceBodyControllerBase.SpaceBodyType type)
        {
            return _missionTypes.Contains(type);
        }
    }
}