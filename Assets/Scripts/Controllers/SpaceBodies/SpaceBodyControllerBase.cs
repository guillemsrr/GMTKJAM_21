using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public abstract class SpaceBodyControllerBase: MonoBehaviour
    {
        public enum SpaceBodyType
        {
            Planet,
            Asteroid,
            Commet,
            Star,
            BlackHole
        }
        
        [SerializeField] private Transform _transform;
        [SerializeField] private GravityAttractor _gravityAttractor;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private SpaceBodyType _type;
        [SerializeField] protected AudioClip _eatAudio;
        [SerializeField] private int _playerDamage = 1;

        
        public Vector3 Position => _transform.position;
        public SpaceBodyType Type => _type;
        public int PlayerDamage => _playerDamage;
        private Vector3 _baseScale; 


        public virtual void Initialize()
        {
            
        }

        public virtual void TriggerEatAudio()
        {
            if (_eatAudio == null) return;
            AudioManager.Instance.Play(_eatAudio);
        }

        public void SetSpaceBodyScale()
        {
            float scale = GetRandomScale();
            SetScale(scale);
        }

        protected float GetRandomScale()
        {
            return Random.Range(0.9f, 2f);
        }

        protected void SetGravityForce(float force)
        {
            _gravityAttractor.SetGravity(-force);
        }

        private void SetScale(float force)
        {
            _transform.localScale = new Vector3(_baseScale.x* force, _baseScale.y*force, _baseScale.z*force);
        }

        public virtual void Destroy()
        {
            CoreManager.Instance.GetLevelGenerator.DestroySpaceBody(_type, this);
            
            //instantiate destroy visual
        }

        protected void Awake()
        {
            _baseScale = _transform.localScale;
        }
    }
}