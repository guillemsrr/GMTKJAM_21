using System.Collections;
using Controllers;
using UnityEngine;

namespace UI
{
    public class BoostController: BarsController
    {
        private PlayerStateHandler _playerStateHandler;
        private readonly WaitForSeconds _boostWaitTime = new WaitForSeconds(1.5f);
        private Coroutine _boostCoroutine;
        private int _boostLevel;

        public void Initialize(PlayerStateHandler playerStateHandler)
        {
            _playerStateHandler = playerStateHandler;
        }

        public void MaxBoost()
        {
            _boostLevel = 2;
            Boost();
        }
        
        public void Boost()
        {
            if(_boostLevel < 3)
                SetBars(++_boostLevel);

            _playerStateHandler.PlayerController.GetSoundTrack._soundTrackVolume = _boostLevel;

            if (_boostCoroutine != null)
            {
                StopCoroutine(_boostCoroutine);
            }
            
            _boostCoroutine = StartCoroutine(StopBoostAfterTime());
        }

        private void DecreaseBoost()
        {
            SetBars(--_boostLevel);
            if (_boostLevel == 0)
            {                
                _playerStateHandler.PlayerController.ResetBoostSpeed();
                _playerStateHandler.PlayerVisualsHandler.BoostScaleOver();
            }

            _playerStateHandler.PlayerController.GetSoundTrack._soundTrackVolume = _boostLevel;
        }
        
        private IEnumerator StopBoostAfterTime()
        {
            while(_boostLevel != 0)
            {
                yield return _boostWaitTime;
                DecreaseBoost();
            }
        }

        public void ResetBoostBars()
        {
            while (_boostLevel > 0)
            {
                DecreaseBoost();
            }
        }
    }
}