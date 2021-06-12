using UnityEngine;

namespace Controllers
{
    public abstract class SpaceBodyControllerBase: MonoBehaviour
    {
        private const float GRAVITY_RADIUS_RELATION = 1f;

        public enum SpaceBodyType
        {
            Planet,
            Asteroid,
            Star,
            BlackHole
        }
        
        [SerializeField] private Transform _transform;
        [SerializeField] private GravityAttractor _gravityAttractor;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private SpaceBodyType _type;
        [SerializeField] protected AudioClip _eatAudio;

        private float _gravityForce;
        
        public Vector3 Position => _transform.position;
        public SpaceBodyType Type => _type;

        public virtual void Initialize()
        {
            
        }

        public virtual void TriggerEatAudio()
        {
            
        }

        public void SetGravityForce(float force)
        {
            _gravityForce = force;
            _gravityAttractor.SetGravity(force);
            _sphereCollider.radius = force * GRAVITY_RADIUS_RELATION;
        }

        public void Destroy()
        {
            Destroy(gameObject);
            
            //instantiate destroy visual
        }
    }
}