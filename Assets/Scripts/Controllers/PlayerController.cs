using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    private float _speed = 100;
    [SerializeField]
    private float _moveLimiter = 0.7f;
    [SerializeField]
    private float _maxSpeed = 10;

    private Vector2 _userInput;

    private Rigidbody _Rigidbody;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();       
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
        if (_Rigidbody.velocity.magnitude < _maxSpeed)
        {
            _Rigidbody.AddForce(force);
        }
    }
}
