using UnityEngine;

namespace Controllers
{
    public class Commet: SpaceBodyControllerBase
    {
        [SerializeField]
        private float _maxSpeed = 5;

        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
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

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("SpaceBody"))
            {
                CoreManager.Instance.GetLevelGenerator.DestroySpaceBody(SpaceBodyType.Commet, this);
            }
        }
    }
}