using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Controllers
{
    public class PlayerMissionsHandler: MonoBehaviour
    {
        private const int MAX_MISSIONS = 8;
        
        [SerializeField] private Transform _missionContainer;
        [SerializeField] private MissionHandler _missionModel;
        [SerializeField] private Text _scoreTxt;

        private int _score = 0;

        private List<MissionHandler> _missions = new List<MissionHandler>();

        private int _currentNumberMissions = 1;
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
                MissionHandler mission = Instantiate(_missionModel, _missionContainer);
                _missions.Add(mission);
                mission.SetMissionType(type);
            }
        }

        private SpaceBodyControllerBase.SpaceBodyType GetRandomType()
        {
            SpaceBodyControllerBase.SpaceBodyType randomType;

            if (Random.value < 0.4f)
            {
                do
                {
                    randomType = RandomEnum.GetRandomFromEnum<SpaceBodyControllerBase.SpaceBodyType>();
                }
                while (randomType == SpaceBodyControllerBase.SpaceBodyType.Star ||
                       randomType == SpaceBodyControllerBase.SpaceBodyType.BlackHole ||
                       randomType == SpaceBodyControllerBase.SpaceBodyType.Planet);
            }
            else
            {
                return SpaceBodyControllerBase.SpaceBodyType.Planet;
            }

            return randomType;
        }
        
        public bool IsTypeInMission(SpaceBodyControllerBase spaceBody)
        {
            foreach (MissionHandler missionHandler in _missions)
            {
                if (missionHandler.IsAccomplished) continue;
                
                if (missionHandler.Compare(spaceBody))
                {
                    _numberMissionAccomplished++;
                    missionHandler.Accomplish();
                    _score += 1 * _numberMissionAccomplished;

                    _scoreTxt.text = "" + _score;
                    return true;
                }
            }

            return false;
        }

        private void Reset()
        {
            foreach (MissionHandler missionHandler in _missions)
            {
                Destroy(missionHandler.gameObject);
            }
            
            _missions.Clear();
        }

        private void Start()
        {
            CreateMissions();
        }
    }
}