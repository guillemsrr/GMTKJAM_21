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

    Transform player;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, m_sphereCollider.radius);
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
}
