using System;
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
        private int _level;
        private bool _isImmune;
        private Queue<SpaceBodyControllerBase> _eatenSpaceBodies = new Queue<SpaceBodyControllerBase>();

        private void Awake()
        {
            DamagedEvent += TakeDamage;
            LevelUpedEvent += LevelUp;
            PlanetEatenEvent += EatPlanet;
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
            _level++;
            
            StartCoroutine(ImmunityTimer());
            _playerVisualsHandler.ChangeVisualFromLevelUp(_level);
            _playerController.SetHigherBaseSpeed();
            _playerController.Boost();
        }

        private void EatPlanet(SpaceBodyControllerBase eatenBody)
        {
            eatenBody.BeEaten(transform);
            _eatenSpaceBodies.Enqueue(eatenBody);
            _playerVisualsHandler.ChangeVisualFromEating(eatenBody.Type);
            _playerController.Boost();
        }

        private IEnumerator ImmunityTimer()
        {
            _isImmune = true;
            yield return _immuneWaitForSeconds;
            _isImmune = false;
        }
    }
}