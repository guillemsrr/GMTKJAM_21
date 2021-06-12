using Controllers;
using UnityEngine;

namespace UI
{
    public class PlayerHUDHandler: MonoBehaviour
    {
        [SerializeField] private PlayerStateHandler _playerStateHandler;

        private PlayerMissionsHandler _missionsHandler;

        private void Awake()
        {
            if (!_playerStateHandler) return;

            _playerStateHandler.DamagedEvent += DamageBars;
            _playerStateHandler.DeadEvent += ApplyGameOver;
            _playerStateHandler.PlanetEatenEvent += EatPlanet;
            _playerStateHandler.LevelUpedEvent += CreateNewMissions;
        }

        private void DamageBars(float damage)
        {
            
        }

        private void ApplyGameOver()
        {
            
        }

        private void EatPlanet(SpaceBodyControllerBase spaceBody)
        {
            if (_missionsHandler.IsTypeInMission(spaceBody.Type))
            {
                _playerStateHandler.EatMissionPlanet();
            }
            else
            {
                _playerStateHandler.EatIncorrectPlanet();
            }
        }

        private void CreateNewMissions()
        {
            _missionsHandler.CreateMissions(_playerStateHandler.Level);
        }
    }
}