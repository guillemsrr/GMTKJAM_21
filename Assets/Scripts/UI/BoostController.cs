using System.Collections;
using UnityEngine;

namespace UI
{
    public class BoostController: BarsController
    {
        [SerializeField] private PlayerController _playerController;
        
        private readonly WaitForSeconds _boostWaitTime = new WaitForSeconds(2f);
        private Coroutine _boostCoroutine;
        private int _boostLevel;
        
        public void Boost()
        {
            if(_boostLevel < 3)
                SetBars(++_boostLevel);

            _playerController.GetSoundTrack._soundTrackVolume = _boostLevel;

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
                _playerController.ResetBoostSpeed();
            }

            _playerController.GetSoundTrack._soundTrackVolume = _boostLevel;
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