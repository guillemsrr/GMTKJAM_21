using System.Collections.Generic;
using UI;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class PlayerMissionsHandler: MonoBehaviour
    {
        private const int MAX_MISSIONS = 8;
        
        [SerializeField] private Transform _missionContainer;
        [SerializeField] private MissionHandler _missionModel;

        private List<SpaceBodyControllerBase.SpaceBodyType> _missionTypes =
            new List<SpaceBodyControllerBase.SpaceBodyType>();

        private List<MissionHandler> _missions = new List<MissionHandler>();

        private int _currentNumberMissions;
        private int _numberMissionAccomplished;
        public bool AreAllMissionsAccomplished => _numberMissionAccomplished == _missions.Count;
        
        public void CreateMissions()
        {
            Reset();
            _numberMissionAccomplished = 0;
            _currentNumberMissions++;
            if (_currentNumberMissions > MAX_MISSIONS)
            {
                _currentNumberMissions = MAX_MISSIONS;
            }
            
            for (int i = 0; i < _currentNumberMissions; i++)
            {
                SpaceBodyControllerBase.SpaceBodyType type = GetRandomType();
                _missionTypes.Add(type);
                MissionHandler mission = Instantiate(_missionModel, _missionContainer);
                _missions.Add(mission);
                mission.SetMissionType(type);
            }
        }

        private SpaceBodyControllerBase.SpaceBodyType GetRandomType()
        {
            SpaceBodyControllerBase.SpaceBodyType randomType;
            do
            {
                randomType = RandomEnum.GetRandomFromEnum<SpaceBodyControllerBase.SpaceBodyType>();
            }
            while (randomType == SpaceBodyControllerBase.SpaceBodyType.BlackHole);

            return randomType;
        }

        public bool IsTypeInMission(SpaceBodyControllerBase.SpaceBodyType type)
        {
            return _missionTypes.Contains(type);
        }

        public void MissionAccomplished()
        {
            _missions[_numberMissionAccomplished].Accomplish();
            _numberMissionAccomplished++;
        }

        private void Reset()
        {
            foreach (MissionHandler missionHandler in _missions)
            {
                Destroy(missionHandler.gameObject);
            }
            
            _missions.Clear();
            _missionTypes.Clear();
        }

        private void Start()
        {
            CreateMissions();
        }
    }
}