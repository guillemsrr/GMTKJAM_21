using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerStateHandler: MonoBehaviour
    {
        public delegate void Damaged(float damageAmount);
        public event Damaged DamagedEvent;
        
        public delegate void LevelUped();
        public event LevelUped LevelUpedEvent;

        public delegate void PlanetEaten(SpaceBodyControllerBase eatenBody);
        public event PlanetEaten PlanetEatenEvent;
        
        public delegate void Dead();
        public event Dead DeadEvent;

        private const int FULL_LIFE = 100;
        
        private readonly WaitForSeconds _immuneWaitForSeconds = new WaitForSeconds(5f);

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerVisualsHandler _playerVisualsHandler;

        private float _life = FULL_LIFE;
        public int Level { get; private set; }
        private bool _isImmune;
        private Queue<SpaceBodyControllerBase> _eatenSpaceBodies = new Queue<SpaceBodyControllerBase>();

        private void Awake()
        {
            DamagedEvent += TakeDamage;
            LevelUpedEvent += LevelUp;
        }

        private void TakeDamage(float damageAmount)
        {
            if (_isImmune) return;
            
            _life -= damageAmount;
            if (_life <= 0)
            {
                _playerVisualsHandler.DeathVisuals();
                DeadEvent?.Invoke();
            }
        }

        private void LevelUp()
        {
            _life = FULL_LIFE;
            Level++;
            
            StartCoroutine(ImmunityTimer());
            _playerVisualsHandler.ChangeVisualFromLevelUp(Level);
            _playerController.SetHigherBaseSpeed();
            _playerController.Boost();
        }

        private void EatPlanet(SpaceBodyControllerBase eatenBody)
        {
            PlanetEatenEvent?.Invoke(eatenBody);
            eatenBody.Destroy();
            _eatenSpaceBodies.Enqueue(eatenBody);
            _playerVisualsHandler.ChangeVisualFromEating(eatenBody.Type);
        }

        public void EatMissionPlanet()
        {
            _playerController.Boost();
        }
        
        public void EatIncorrectPlanet()
        {
        }

        private IEnumerator ImmunityTimer()
        {
            _isImmune = true;
            yield return _immuneWaitForSeconds;
            _isImmune = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("SpaceBody"))
            {
                EatPlanet(other.GetComponent<SpaceBodyControllerBase>());
            }
        }
    }
}