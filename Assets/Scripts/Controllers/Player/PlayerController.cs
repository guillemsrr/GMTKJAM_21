using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float SPEED_AUGMENT = 1f;
    private const float MAX_BOOST_AUGMENT = 3f;
    
    [SerializeField]
    private float _speed = 100;
    [SerializeField]
    private float _moveLimiter = 0.7f;
    [SerializeField]
    private float _maxSpeed = 10;

    private float _boostSpeed;

    private Vector2 _userInput;

    private Rigidbody _rigidbody;
    private SoundTrack _soundTrack;
    public SoundTrack GetSoundTrack { get { return _soundTrack; } }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _soundTrack = GetComponent<SoundTrack>();
    }

    void FixedUpdate()
    {
        ReadInputs();
        UpdatePlayer();
    }

    private void ReadInputs()
    {       
        _userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void UpdatePlayer()
    {
        Vector3 movement = new Vector3(_userInput.x, 0, _userInput.y);

        if (movement.x != 0 && movement.z != 0)
        {
            movement *= _moveLimiter;
        }

        AddForce(movement * _speed * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        _rigidbody.AddForce(force);
        if (_rigidbody.velocity.magnitude > _maxSpeed + _boostSpeed)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed + _boostSpeed);
        }
    }

    public void SetHigherBaseSpeed()
    {
        _maxSpeed += SPEED_AUGMENT;
    }

    public void ApplyBoostSpeed()
    {
        if (_boostSpeed >= MAX_BOOST_AUGMENT) return;
        
        _boostSpeed += SPEED_AUGMENT;
    }
    
    public void ResetBoostSpeed()
    {
        _boostSpeed = 0;
    }
}
