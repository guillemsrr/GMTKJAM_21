using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 userInput;
    [SerializeField]
    private float speed = 100;
    [SerializeField]
    private float moveLimiter = 0.7f;
    [SerializeField]
    private float maxSpeed = 10;

    Rigidbody m_Rigidbody;



    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();       
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        UpdatePlayer();
    }

    private void ReadInputs()
    {
       
        userInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void UpdatePlayer()
    {
        Vector3 movement = new Vector3(userInput.x, 0, userInput.y);

        if (movement.x != 0 && movement.z != 0)
        {
            movement *= moveLimiter;
        }

        AddForce(movement * speed * Time.deltaTime);
    }

    public void AddForce(Vector3 force)
    {
        if (m_Rigidbody.velocity.magnitude < maxSpeed)
        {
            m_Rigidbody.AddForce(force);
        }
    }
}
