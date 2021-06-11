using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 userInput;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float moveLimiter = 0.7f;

    Rigidbody m_Rigidbody;

    GravityAttractor attractor;
    public bool dontAffectForce;

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

    private void FixedUpdate()
    {
 
        if(attractor && !dontAffectForce)
            attractor.Attract(transform);
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

        Vector3 camForward = Vector3.Scale(Camera.main.transform.up, new Vector3(1, 0, 1)).normalized;
        Vector3 move = userInput.y * camForward + userInput.x * Camera.main.transform.right;

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        Vector3 localMove = transform.InverseTransformDirection(move);
    }

    private void OnTriggerEnter(Collider other)
    {
        dontAffectForce = false;
        attractor = other.GetComponent<GravityAttractor>();

    }
    private void OnTriggerExit(Collider other)
    {
        dontAffectForce = true;
        attractor = null;
    }
}
