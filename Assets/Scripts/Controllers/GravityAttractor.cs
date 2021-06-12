using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField]
    private float gravity = -10f;
    [SerializeField]
    private float mass;
    [SerializeField]
    private SphereCollider m_sphereCollider;

    public bool dontAffectForce;

    [SerializeField]
    private Material triggerMat;

    Transform player;
    private GameObject trigger;

    

    private void Start()
    {
        trigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        trigger.transform.parent = transform;
        trigger.GetComponent<Renderer>().material = triggerMat;
        trigger.transform.position = transform.position;
        trigger.transform.localScale *= m_sphereCollider.radius*2;
        trigger.SetActive(false);

        CoreManager.Instance.OnIsDebug += HandlerIsDebug;        
    }
    public void HandlerIsDebug(object sender, EventArgs e)
    {
        trigger.SetActive(!trigger.active);
    }

    public void Attract(Transform body)
    {
        if(player)
            Attract(body, gravity, false, false);
    }

    public void Attract(Transform body, float customGravity, bool dontAffectForce, bool dontAffectRot)
    {
        Vector3 targetDir = (body.position - transform.position).normalized;

        Vector3 bodyUp = body.up;
        Quaternion targetRotation = Quaternion.identity;
        if (!dontAffectForce)
        {
            body.GetComponent<PlayerController>().AddForce(targetDir * customGravity);
        }
        if (!dontAffectRot)
            targetRotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 1);
    }

    private void FixedUpdate()
    {
        if (!dontAffectForce)
            Attract(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            dontAffectForce = false;
            player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            dontAffectForce = true;
            player = null;
        }
    }

    private void OnDestroy()
    {
        CoreManager.Instance.OnIsDebug -= HandlerIsDebug;
    }

}
