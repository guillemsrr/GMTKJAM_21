using UnityEngine;

namespace Controllers.AI
{
    public class AIDifficultyController: MonoBehaviour
    {
        [SerializeField] private PlayerStateHandler _playerStateHandler;

        private void Awake()
        {
            _playerStateHandler.LevelUpedEvent += SetHigherDifficulty;
        }

        private void SetHigherDifficulty()
        {
            
        }
    }
}