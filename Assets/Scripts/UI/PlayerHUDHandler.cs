using Controllers;
using UnityEngine;

namespace UI
{
    public class PlayerHUDHandler: MonoBehaviour
    {
        [SerializeField] private PlayerStateHandler _playerStateHandler;
        [SerializeField] private PlayerMissionsHandler _missionsHandler;
        [SerializeField] private BarsController _lifeController;
        [SerializeField] private BoostController _boostController;
        [SerializeField] private GameObject _gameOverObject;

        private void Awake()
        {
            if (!_playerStateHandler) return;

            _playerStateHandler.DeadEvent += ApplyGameOver;
            _playerStateHandler.BodyEatenEvent += EatPlanet;
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
                _boostController.Boost();
                
                if (_missionsHandler.AreAllMissionsAccomplished)
                {
                    _playerStateHandler.LevelUp();
                    _missionsHandler.CreateMissions();
                }
            }
            else
            {
                _playerStateHandler.EatIncorrectPlanet(spaceBody);
                _boostController.ResetBoostBars();
            }
            
            _lifeController.SetBars(_playerStateHandler.Life);
        }
    }
}