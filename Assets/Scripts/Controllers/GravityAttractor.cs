using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    [SerializeField]
    private float _gravity = -10f;
    [SerializeField]
    private float _mass;
    [SerializeField]
    private SphereCollider _sphereCollider;

    public bool _dontAffectForce;

    [SerializeField]
    private Material _triggerMat;

    Transform _player;
    private GameObject _trigger;

    private CoreManager _coreManager;

    private void Start()
    {
        _trigger = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        _trigger.transform.parent = transform;
        _trigger.GetComponent<Renderer>().material = _triggerMat;
        _trigger.transform.position = transform.position;
        _trigger.transform.localScale *= _sphereCollider.radius*2;
        _trigger.SetActive(false);

        _coreManager = CoreManager.Instance;
        _coreManager.OnIsDebug += HandlerIsDebug;        
    }
    public void HandlerIsDebug(object sender, EventArgs e)
    {
        _trigger.SetActive(!_trigger.activeSelf);
    }

    public void Attract(Transform body)
    {
        if(_player)
            Attract(body, _gravity, false, false);
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

    public void SetGravity(float force)
    {
        _gravity = force;
    }

    private void FixedUpdate()
    {
        if (!_dontAffectForce)
            Attract(_player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _dontAffectForce = false;
            _player = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            _dontAffectForce = true;
            _player = null;
        }
    }

    private void OnDestroy()
    {
        _coreManager.OnIsDebug -= HandlerIsDebug;
    }

}
