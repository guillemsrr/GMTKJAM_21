using System.Collections;
using System.Collections.Generic;
using Controllers.VFX;
using UnityEngine;

namespace Controllers
{
    public class PlayerStateHandler: MonoBehaviour
    {
        public delegate void Damaged(int damageAmount);
        public event Damaged DamagedEvent;
        
        public delegate void LevelUped();
        public event LevelUped LevelUpedEvent;

        public delegate void BodyEaten(SpaceBodyControllerBase eatenBody);
        public event BodyEaten BodyEatenEvent;
        
        public delegate void Dead();
        public event Dead DeadEvent;
        

        private const int FULL_LIFE = 3;
        
        private readonly WaitForSeconds _immuneWaitForSeconds = new WaitForSeconds(5f);

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerVisualsHandler _playerVisualsHandler;
        [SerializeField] private AudioClip _deadClip;
        [SerializeField] private AudioClip _eatErrorClip;
        [SerializeField] private TemporalVFX _eaTemporalVFX;

        private int _life = FULL_LIFE;
        public int Level { get; private set; }

        private bool _isImmune;
        private Queue<SpaceBodyControllerBase> _eatenSpaceBodies = new Queue<SpaceBodyControllerBase>();

        private bool IsDead => _life <= 0;
        public int Life => _life;
        public PlayerController PlayerController => _playerController;
        public PlayerVisualsHandler PlayerVisualsHandler => _playerVisualsHandler;

        private bool _isFirtFault = true;
        private bool _isFirtCorrect = true;

        private void Awake()
        {
            DamagedEvent += TakeDamage;
        }

        private void TakeDamage(int damageAmount)
        {
            if (_isImmune) return;
            
            _life -= damageAmount;
            
            if (IsDead)
            {
                _playerController.enabled = false;
                _playerVisualsHandler.DeathVisuals();
                AudioManager.Instance.Play(_deadClip);
                DeadEvent?.Invoke();
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
            _playerController.ApplyBoostSpeed();
        }

        private void EatSpaceBody(SpaceBodyControllerBase eatenBody)
        {
            BodyEatenEvent?.Invoke(eatenBody);
            eatenBody.Destroy();
            _eatenSpaceBodies.Enqueue(eatenBody);         
            eatenBody.TriggerEatAudio();
        }

        public void EatMissionPlanet()
        {
            _playerVisualsHandler.ChangeVisualFromEating(true);
            _playerController.ApplyBoostSpeed();

            if (_isFirtCorrect)
            {
                _isFirtCorrect = false;
                CoreManager.Instance.GetDialogeCanvas.ActivateCanvasWithText("Correct! eat the next body!");
                CoreManager.Instance.GetDialogeCanvas.DeactivateCanvasWithDelay(5f);
            }
        }
        
        public void EatIncorrectPlanet(SpaceBodyControllerBase eatenBody)
        {
            DamagedEvent?.Invoke(eatenBody.PlayerDamage);
            _playerController.ResetBoostSpeed();

            if (_isFirtFault)
            {
                _isFirtFault = false;
                CoreManager.Instance.GetDialogeCanvas.ActivateCanvasWithText("Incorrect Space body");
                CoreManager.Instance.GetDialogeCanvas.DeactivateCanvasWithDelay(5f);
            }
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
                TemporalVFX explosion = Instantiate(_eaTemporalVFX);
                explosion.transform.position = transform.position;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                LevelManager.Instance.LoadGame();
            }
        }
    }
}