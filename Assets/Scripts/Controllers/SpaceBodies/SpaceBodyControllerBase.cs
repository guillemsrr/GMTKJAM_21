using UnityEngine;

namespace Controllers
{
    public class SpaceBodyControllerBase: MonoBehaviour
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

        private float _gravityForce;
        
        public Vector3 Position => _transform.position;
        public SpaceBodyType Type => _type;

        public void SetGravityForce(float force)
        {
            _gravityForce = force;
            _gravityAttractor.SetGravity(force);
            _sphereCollider.radius = force * GRAVITY_RADIUS_RELATION;
            _transform.localScale = Vector3.up * _gravityForce;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}