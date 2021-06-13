using UnityEngine;

namespace Controllers
{
    public class PlayerVisualsHandler: MonoBehaviour
    {
        [SerializeField]
        private GameObject _gameEffects;
        [SerializeField]
        private GameObject _vfx;

        public void ChangeVisualFromLevelUp(int level)
        {
            
        }
        
        public void ChangeVisualFromEating(bool isCorrect)
        {
            if (isCorrect)
            {
                _gameEffects.SetActive(false);
                _gameEffects.SetActive(true);
            }
        }

        public void DeathVisuals()
        {
            _vfx.SetActive(false);

            CoreManager.Instance.GetDialogeCanvas.ActivateCanvasWithText("Press R for Restart");
        }
    }
}