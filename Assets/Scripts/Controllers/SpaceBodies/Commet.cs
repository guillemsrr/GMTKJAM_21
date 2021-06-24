using Controllers.VFX;
using UnityEngine;

namespace Controllers
{
    public class Commet: SpaceBodyControllerBase
    {
        [SerializeField]
        private float _maxSpeed = 5;

        [SerializeField] private TemporalVFX _destroyVFX;

        [SerializeField] private Transform _destroyTransform;

        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            AddForce((Vector3.forward  + Vector3.left) * 100);
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude == 0)
            {
                AddForce((Vector3.forward + Vector3.left) * 100);
                return;
            }
            
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }

        public void AddForce(Vector3 force)
        {
            _rigidbody.AddForce(force);
            if (_rigidbody.velocity.magnitude > _maxSpeed)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.tag.Equals("SpaceBody"))
            {
                TemporalVFX explosion = Instantiate(_destroyVFX);
                explosion.transform.position = _destroyTransform.position;
                CoreManager.Instance.GetLevelGenerator.DestroySpaceBody(SpaceBodyType.Commet, this);
            }
        }
    }
}