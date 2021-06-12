using Controllers;
using UnityEngine;

namespace UI
{
    public class PlayerHUDHandler: MonoBehaviour
    {
        [SerializeField] private PlayerStateHandler _playerStateHandler;
        [SerializeField] private PlayerMissionsHandler _missionsHandler;
        [SerializeField] private LifeController _lifeController;
        [SerializeField] private GameObject _gameOverObject;

        private void Awake()
        {
            if (!_playerStateHandler) return;

            _playerStateHandler.DeadEvent += ApplyGameOver;
            _playerStateHandler.BodyEatenEvent += EatPlanet;
        }

        private void DamageBars(float damage)
        {
            
        }

        private void ApplyGameOver()
        {
            _gameOverObject.SetActive(true);
        }

        private void EatPlanet(SpaceBodyControllerBase spaceBody)
        {
            if (_missionsHandler.IsTypeInMission(spaceBody.Type))
            {
                _playerStateHandler.EatMissionPlanet();
                _missionsHandler.MissionAccomplished();
                
                if (_missionsHandler.AreAllMissionsAccomplished)
                {
                    _playerStateHandler.LevelUp();
                    _missionsHandler.CreateMissions(_playerStateHandler.Level);
                }
            }
            else
            {
                _playerStateHandler.EatIncorrectPlanet(spaceBody);
                DamageBars(spaceBody.PlayerDamage);
            }
        }
    }
}