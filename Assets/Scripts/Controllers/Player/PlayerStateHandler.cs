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

        private const int FULL_LIFE = 9;
        
        private readonly WaitForSeconds _immuneWaitForSeconds = new WaitForSeconds(5f);

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerVisualsHandler _playerVisualsHandler;
        [SerializeField] private AudioClip _deadClip;
        [SerializeField] private AudioClip _eatErrorClip;

        private float _life = FULL_LIFE;
        public int Level { get; private set; }

        private bool _isImmune;
        private Queue<SpaceBodyControllerBase> _eatenSpaceBodies = new Queue<SpaceBodyControllerBase>();

        private bool IsDead => _life <= 0;

        private void Awake()
        {
            DamagedEvent += TakeDamage;
        }

        private void TakeDamage(float damageAmount)
        {
            if (_isImmune) return;
            
            _life -= damageAmount;
            Debug.LogError("_life: " + _life);
            
            if (IsDead)
            {
                _playerController.enabled = false;
                _playerVisualsHandler.DeathVisuals();
                AudioManager.Instance.Play(_deadClip);
                DeadEvent?.Invoke();
                Debug.LogError("DEAD");
            }
            else
            {
                AudioManager.Instance.Play(_eatErrorClip);
            }
        }

        public void LevelUp()
        {
            LevelUpedEvent?.Invoke();
            
            _life = FULL_LIFE;
            Level++;
            
            StartCoroutine(ImmunityTimer());
            _playerVisualsHandler.ChangeVisualFromLevelUp(Level);
            _playerController.SetHigherBaseSpeed();
            _playerController.Boost();
        }

        private void EatSpaceBody(SpaceBodyControllerBase eatenBody)
        {
            PlanetEatenEvent?.Invoke(eatenBody);
            eatenBody.Destroy();
            _eatenSpaceBodies.Enqueue(eatenBody);
           _playerVisualsHandler.ChangeVisualFromEating(eatenBody.Type);
            eatenBody.TriggerEatAudio();
        }

        public void EatMissionPlanet()
        {
            _playerController.Boost();
        }
        
        public void EatIncorrectPlanet(SpaceBodyControllerBase eatenBody)
        {
            DamagedEvent?.Invoke(eatenBody.PlayerDamage);
        }

        private IEnumerator ImmunityTimer()
        {
            _isImmune = true;
            yield return _immuneWaitForSeconds;
            _isImmune = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsDead) return;
            
            if (other.tag.Equals("SpaceBody"))
            {
                EatSpaceBody(other.GetComponent<SpaceBodyControllerBase>());
            }
        }
    }
}